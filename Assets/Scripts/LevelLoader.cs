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
    public int numberOfColumns = 1;
    public TMP_Text result;
    public static bool loadingLevel;
    //all spawnable objects
    public GameObject startPrefab, endPrefab, blockPrefab, repeaterPrefab,platformPrefab, resisterPrefab;

    int levelIndex = 0;
    void Start()
    {
        MakeLevel();
        GameManager.loadNextLvl += goToNext;

    }
    public void MakeLevel()
    {
        string[] data = levelData[levelIndex].text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

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
        int tableSize = (data.Length / numberOfColumns); //got all the individual cell as an element from the file. Divde that number by column number u get the row numb;



        GameObject platForm = Instantiate(platformPrefab);

        platForm.transform.localScale = new Vector3(tableSize, 1, numberOfColumns);



        for (int i = 0; i < tableSize; i++) //go through each line and generate it
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                //basing off of the coordinates and calculate the index that it would in the string array;
                string currentSquare = data[(i * numberOfColumns) + j];
                //Debug.Log(currentSquare);

                Vector3 newVector = new Vector3(j, 0.41f, -(i));

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
                            Debug.Log(repeater.transform.up);
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

        loadingLevel = false;
    }
    void goToNext()
    {
        StartCoroutine(loading());
    }
    IEnumerator loading()
    {
        yield return new WaitForSeconds(1.5f);
        advaneceLevel();

        yield return new WaitForSeconds(1.5f);

        if(levelIndex < lvStats.Length - 1) MakeLevel();
        

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

        if(levelIndex < lvStats.Length -1)
        {   
            levelIndex++;
            
        }
       
    }

}
