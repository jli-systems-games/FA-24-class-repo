using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wall;
    public GameObject spawns, grips,sGrips;
    int width, height;
    int count;
    List<Vector3> locations = new List<Vector3>();
    List<Vector3> locations2 = new List<Vector3>();
    List<Vector3> existingLoc = new List<Vector3>();

    void Start()
    {
        spawnObjects();

    }
    void spawnObjects()
    {
        width = (int)wall.transform.localScale.x;
        height = (int)wall.transform.localScale.y;
        int goal = 2;

        GameObject WallP = Instantiate(wall);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height + 1; j++)
            {
                if (j == 1)
                {
                    
                    Vector3 loc = new Vector3(i, j, wall.transform.localPosition.z + 1);
                    locations.Add(loc);
                    // Debug.Log(loc);
                }

                if (j == height - 5)
                {
                    Vector3 loc = new Vector3(i, j, wall.transform.localPosition.z + 1);
                    locations2.Add(loc);

                }

            }
        }

        int startIn = UnityEngine.Random.Range(0, locations.Count - 1);
        GameObject obj = Instantiate(spawns);
        obj.transform.position = locations[startIn];
        Transform x1 = obj.transform;
        existingLoc.Add(x1.position);

        int endIn = UnityEngine.Random.Range(0, locations2.Count - 1);
        //Debug.Log(endIn);
        GameObject _eobj = Instantiate(spawns);
        _eobj.transform.position = locations2[endIn];
        Transform y1 = _eobj.transform;
        
        //added the location into the list
        existingLoc.Add(y1.position);

        //calculate the regression line between the 2 points
        float run = x1.localPosition.x - y1.localPosition.x;
        float slope = (x1.localPosition.y - y1.localPosition.y) / run;
        
        //Debug.Log(run);
        if(Mathf.Abs(run) > 19f)
        {
            goal = 4;
        }
        float intercept = x1.localPosition.y - (slope * x1.localPosition.x);

        spawnBigGrips(slope, intercept, x1.position, x1.position, y1.position,goal);
        findGap();

    }
    void spawnBigGrips(float m, float b, Vector3 currStart, Vector3 prevStart, Vector3 End, int goal)
    {
        count++;

        if (count > goal) return;

        Vector3[] Range = chooseSpawnDirection(currStart, prevStart, End);
        float x1 = Range[0].x;
        float x2 = Range[1].x;

        float xRange = x2 - x1;
        //check for edge case of it being zero, it should never be zero;
        if (xRange == 0)
        {
            xRange = 5;
        }
        //Debug.Log(xRange);

        //use to make sure x don't go out of the area of the wall
        int[] bounds = GreaterLesser((int)x1, (int)x2);


        float random = Mathf.Clamp(UnityEngine.Random.value, 0.45f, 0.75f);
        float X = x1 + (xRange * random);
         
        //rerolling x 
        /*while ((int)X == (int)Range[2].x)
        {
            X = x1 + (xRange * random);
        }*/

       // Debug.Log("X" + X + "lastX" + Range[2].x);
        X = Mathf.Clamp(X, bounds[0], bounds[1] - 2);

        float Y = X * m + b;
        float diffY = Range[2].y - Y;
        if (Mathf.Abs(diffY) < 3)
        {
            Y = adjustY(diffY, Y);
            X = (Y - b) / m;
            /*int[] ybounds = GreaterLesser((int)Range[0].y, (int)Range[1].y);
            Y = Mathf.Clamp(Y, ybounds[0] + 2, ybounds[1] - 2);*/
        }
        GameObject obj = Instantiate(grips);
        obj.transform.position = new Vector3(X, Y, wall.transform.localPosition.z + 1);
        existingLoc.Add(obj.transform.position);

        spawnBigGrips(m, b, obj.transform.position, Range[0], Range[1], goal);
        
    }
    Vector3[] chooseSpawnDirection(Vector3 CurrentStartSpawn, Vector3 LastStartSpawn, Vector3 EndSpawn)
    {
        Vector3[] Range = { LastStartSpawn, EndSpawn, CurrentStartSpawn};

        float uppDif = EndSpawn.y - CurrentStartSpawn.y;
        float downDif = CurrentStartSpawn.y - LastStartSpawn.y;

        if (uppDif > downDif) Range[0] = CurrentStartSpawn;
        else Range[1] = CurrentStartSpawn;

        //Range[2] = LastStartSpawn;

        return Range;
    }

    int[] GreaterLesser(int i, int j)
    {
        int[] numb = new int[2];
        if (i > j)
        {
            numb[0] = j;
            numb[1] = i;
        }
        else
        {
            numb[0] = i;
            numb[1] = j;
        }

        return numb;
    }
    float adjustY(float currDiff, float currY)
    {   
        float y;
        Debug.Log("Ydiff" + currDiff);
        float magnitude = 12 - currDiff;
        Debug.Log("mag" + magnitude);
        if (currDiff <= 0) y = currY + magnitude;
        else y = currY - magnitude;

        return y;
    }

    void findGap()
    {
        existingLoc.Sort((x,y) => x.y.CompareTo(y.y));
        int max = 0;
        float delta = 0;
        List<Pair> pairs = new List<Pair>();

        for(int i = 0; i < existingLoc.Count-1; i++)
        {
               
           delta = Vector3.Distance(existingLoc[i], existingLoc[i + 1]);
           max = max + (int)delta;
           

        }

        float avg = max / existingLoc.Count;
        for (int i = 0; i < existingLoc.Count - 1; i++)
        {
            delta = Vector3.Distance(existingLoc[i], existingLoc[i + 1]);
            if(delta > avg)
            {
                pairs.Add(new Pair(existingLoc[i], existingLoc[i + 1]));

                
            }

        }

        foreach (Pair l in pairs)
        {
           RowSpawn(l.End, l.Start, sGrips);
           // Debug.Log("org" + l.End + " & End" + l.Start);
        }
    }

    void RowSpawn(Vector3 origin, Vector3 endPoint, GameObject sHold)
    {
        float _x = UnityEngine.Random.Range(origin.x -1f, origin.x + 2f);
        float _y = origin.y - 1f;

        GameObject child = Instantiate(sHold);
        sHold.transform.position = new Vector3(_x, _y, origin.z);
        //Debug.Log("fin" + sHold.transform.position.x);

        if (Vector3.Distance(origin, endPoint) < 3) Debug.Log("stopping");
    }
}
