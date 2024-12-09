using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public TextAsset[] levelData;
    public levelStats[] lvStats;
    int numberOfRows = 1;
    public TMP_Text result, lvNumb;
    public static bool loadingLevel;
    //all spawnable objects
    public GameObject startPrefab, endPrefab, blockPrefab, repeaterPrefab,platformPrefab, resisterPrefab;
    public Transform leftWall, rightWall, TopWall, BottomWall;
    Vector3 ogL, ogR, ogT, ogB;
    int levelIndex = 0;
    bool reload = false;
    void Start()
    {   
        ogL = leftWall.position;
        ogR = rightWall.position;
        ogT = TopWall.position;
        ogB = BottomWall.position;  
        MakeLevel();
        
        GameManager.loadNextLvl += goNext;

    }
    public void MakeLevel()
    {
        string[] data = levelData[levelIndex].text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        numberOfRows = lvStats[levelIndex].rows;
        //Debug.Log(data.Length);
        //clean up the array;
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].Contains("\r"))
            {
                data[i] = data[i].Replace("\r", "");
                //Debug.Log(data[i]);
            }
        }
        int tableSize = (data.Length / numberOfRows); //got all the individual cell as an element from the file. Divde that number by column number u get the row numb;



        GameObject platForm = Instantiate(platformPrefab);

        platForm.transform.localScale = new Vector3(tableSize, 1, numberOfRows);

        //redo the walls
        if (tableSize > 4)
        {
              //recalculate the position for side blocks;
              float x = ((float)tableSize - 4) * 0.5f;
            leftWall.position = leftWall.position + new Vector3(-x, 0, 0);
            leftWall.localScale = leftWall.localScale + new Vector3(0, 0, (float)tableSize - 4);

            rightWall.position = rightWall.position + new Vector3(x, 0, 0);
            rightWall.localScale = rightWall.localScale + new Vector3(0, 0, (float)tableSize - 4);


        }
      

        if(numberOfRows > 4)
        {
            float y = ((float)numberOfRows - 4) * 0.5f;
            TopWall.position = TopWall.position + new Vector3(0, 0, y);
            TopWall.localScale = TopWall.localScale + new Vector3((float)numberOfRows - 4, 0, 0);

            BottomWall.position = BottomWall.position + new Vector3(0, 0, -y);
            BottomWall.localScale = BottomWall.localScale + new Vector3((float)numberOfRows - 4, 0, 0);
        }
      

        for (int i = 0; i < numberOfRows; i++) //go through each line and generate it
        {
            for (int j = 0; j < tableSize; j++)
            {
                //basing off of the coordinates and calculate the index that it would in the string array;
                string currentSquare = data[(i * tableSize) + j];
                //Debug.Log(currentSquare);
                Vector3 newVector = new Vector3(j, 0.41f, -(i));

                if(tableSize > 4)
                {
                    float _x = ((float)tableSize - 4) * 0.5f;
                    newVector = newVector + new Vector3(- _x,0,0);
                  
                }

                if (numberOfRows > 4) 
                { 
                    float _z = ((float)numberOfRows - 4) * 0.5f;
                    newVector = newVector + new Vector3(0, 0, _z); 
                }


                if (currentSquare != "") //skip over empty areas
                {
                    //Debug.Log("This is " + currentSquare + currentSquare.GetType() );

                        switch (currentSquare)
                        {
                            case "S":
                                //spawn starting points
                                GameObject start = Instantiate(startPrefab, newVector, Quaternion.identity);
                                start.transform.SetParent(platForm.transform, true);
                                PlayerHit hitComp = start.transform.Find("hitBox").GetComponent<PlayerHit>();
                                if (hitComp != null)
                            {
                                hitComp.Energy = lvStats[levelIndex].maxEnergy;
                                hitComp.MaxEnergy = lvStats[levelIndex].maxEnergy;
                            }

                                break;
                            case "F":
                                //spawn end point
                                GameObject end = Instantiate(endPrefab, newVector, Quaternion.identity);
                                end.transform.SetParent(platForm.transform, true);
                                DeteEnd dE = end.GetComponent<DeteEnd>();
                            if (dE != null)
                            {
                                dE.target = lvStats[levelIndex].targetEnergy;
                                dE._t = result;
                            }

                                break;
                            case "B":
                                //instantiate blocks obstacles
                                GameObject blocks = Instantiate(blockPrefab, newVector, Quaternion.identity);
                                blocks.transform.SetParent(platForm.transform, true);
                                break;
                            case "HR":
                                //instantiate Repeater
                                GameObject repeater = Instantiate(repeaterPrefab);
                                Quaternion rot = Quaternion.identity;
                                rot.eulerAngles = new Vector3(90, 0, -90);
                                repeater.transform.rotation = rot;
                               
                                repeater.transform.SetParent (platForm.transform, true);
                            //Debug.Log(repeater.transform.up);
                                repeater.transform.position = newVector;
                                break;
                        case "VR":
                            GameObject vRepeater = Instantiate(repeaterPrefab);
                            vRepeater.transform.position = newVector;
                            vRepeater.transform.SetParent(platForm.transform, true);
                            break;
                        case "RE":
                            GameObject resis = Instantiate(resisterPrefab);
                            resis.transform.position = newVector;
                            resis.transform.SetParent(platForm.transform, true);
                            break;

                            default:
                                break;
                        }
                    
                }
            }

        }

        lvNumb.text = (levelIndex + 1).ToString();
        if (reload)
        {
            loadingLevel = false;
            reload = false;
        }
    }
    public void reloadCurrentLevel()
    {
        reload = true;
        InputManager.points.Clear();
        StartCoroutine(loading());
    }

    void goNext()
    {   
        StartCoroutine(loading());
        
    }
    IEnumerator loading()
    {
        yield return new WaitForSeconds(1.5f);

        //combine both of them into one function and call it after the brain loading is done;
        advaneceLevel();
        yield return new WaitForSeconds(1f);
        resetStat();
        yield return new WaitForSeconds(1.5f);

        if(levelIndex <= lvStats.Length - 1) MakeLevel();
    }

    public void advaneceLevel()
    {
        //find the current platformParent;
        loadingLevel = true;
        GameObject ply = GameObject.FindWithTag("platform");
        if (ply != null)
        {
            foreach (Transform t in ply.transform)
            {
                Destroy(t.gameObject);
            }
            Destroy(ply);
           
        }

        if(levelIndex <= lvStats.Length -1 && !reload)
        {   
            levelIndex++;
            //Debug.Log(levelIndex);
        }
       
    }

    void resetStat()
    {
        LineRenderer[] lns = UnityEngine.Object.FindObjectsByType<LineRenderer>(FindObjectsSortMode.None);
        if (lns.Length > 0)
        {
            foreach (LineRenderer ln in lns)
            {
                if (ln.gameObject.name.Contains("newLine")) Destroy(ln.gameObject);


            }
        }

        //reset the wall locations;
        leftWall.position = ogL;
        leftWall.localScale = new Vector3(1, 1.4f, 4);
        rightWall.position = ogR;
        rightWall.localScale = new Vector3(1, 1.4f, 4);

        TopWall.position = ogT;
        TopWall.localScale = new Vector3(4, 1.4f, 1);
        BottomWall.position = ogB;
        BottomWall.localScale = new Vector3(4, 1.4f, 1);
    }

}
