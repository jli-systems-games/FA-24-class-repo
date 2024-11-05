using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wall;
    public GameObject spawns, grips;
    int width, height;
    int count;
    List<Vector3> locations = new List<Vector3>();
    List<Vector3> locations2 = new List<Vector3>();
    void Start()
    {
        spawnObjects();

    }
    void spawnObjects()
    {
        width = (int)wall.transform.localScale.x;
        height = (int)wall.transform.localScale.y;
        GameObject WallP = Instantiate(wall);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height + 1; j++)
            {
                if (j == 1)
                {
                    /* GameObject _obj = Instantiate(spawns);
                      _obj.transform.localPosition  = new Vector3(i,j,wall.transform.localPosition.z);
                      _obj.transform.SetParent(WallP.transform);*/
                    Vector3 loc = new Vector3(i, j, wall.transform.localPosition.z - 1);
                    locations.Add(loc);
                    // Debug.Log(loc);
                }

                if (j == height)
                {
                    Vector3 loc = new Vector3(i, j, wall.transform.localPosition.z - 1);
                    locations2.Add(loc);

                }

            }
        }

        int startIn = Random.Range(0, locations.Count - 1);
        GameObject obj = Instantiate(spawns);
        obj.transform.position = locations[startIn];
        Transform x1 = obj.transform;

        int endIn = Random.Range(0, locations2.Count - 1);
        //Debug.Log(endIn);
        GameObject _eobj = Instantiate(spawns);
        _eobj.transform.position = locations2[endIn];
        Transform y1 = _eobj.transform;

        //calculate the regression line between the 2 points
        float slope = (x1.localPosition.y - y1.localPosition.y) / (x1.localPosition.x - y1.localPosition.x);

        float intercept = x1.localPosition.y - (slope * x1.localPosition.x);

        spawnBigGrips(slope, intercept, x1.position, x1.position, y1.position);


    }
    void spawnBigGrips(float m, float b, Vector3 currStart, Vector3 prevStart, Vector3 End)
    {
        count++;

        if (count > 3) return;

        Vector3[] Range = chooseSpawnDirection(currStart, prevStart, End);
        float x1 = Range[0].x;
        float x2 = Range[1].x;

        float xRange = x2 - x1;
        //check for edge case of it being zero, it should never be zero;
        if (xRange == 0)
        {
            xRange = 5;
        }
        Debug.Log(xRange);
        int[] bounds = GreaterLesser((int)x1, (int)x2);

        float X = x1 + (xRange * Mathf.Clamp(Random.value, 0.75f, 1f));

        X = Mathf.Clamp(X, bounds[0], bounds[1] - 2);

        float Y = X * m + b;

        GameObject obj = Instantiate(grips);
        obj.transform.position = new Vector3(X, Y, wall.transform.localPosition.z - 1);

        spawnBigGrips(m, b, obj.transform.position, Range[0], Range[1]);

    }
    Vector3[] chooseSpawnDirection(Vector3 CurrentStartSpawn, Vector3 LastStartSpawn, Vector3 EndSpawn)
    {
        Vector3[] Range = { LastStartSpawn, EndSpawn };

        float uppDif = EndSpawn.y - CurrentStartSpawn.y;
        float downDif = CurrentStartSpawn.y - LastStartSpawn.y;

        if (uppDif > downDif) Range[0] = CurrentStartSpawn;
        else Range[1] = CurrentStartSpawn;


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
}
