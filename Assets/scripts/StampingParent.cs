using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StampingParent : MonoBehaviour
{ 

    public EventManagers manage;
    public List<int> Score = new List<int>();

    List<Transform> children = new List<Transform>();
    Vector3 ogPosition;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (manage.firstpass)
        {
            foreach(Transform t in transform)
            {
                children.Add(t);
            }
        }
        else
        {
            //Debug.Log("start to ranomize");
            RandomizeandCopy();
        }
    }
    void Start()
    {
        ogPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void RandomizeandCopy()
    {
        
        //children are all the individual objects with an overall empty book parent
        int len = children.Count;
        int index = Random.Range(0, len);

        GameObject nBook = Instantiate(children[index].gameObject);
        nBook.transform.SetParent(transform,true);
        nBook.transform.position = new Vector3(transform.position.x, transform.position.y,0) ;
        children.Add(nBook.transform);
        len = children.Count;

        //these are the new random z positions.
        List<int> newSibIndexes = randomize(len);
        
        for (int i = 0; i < len; i++)
        {
            
                children[i].position = new Vector3(transform.position.x, transform.position.y, newSibIndexes[i]);
            
        }

     
    }

    List<int> randomize(int n)
    {
        List<int> randomN = new List<int>();
        int amount = 0;
        do
        {   
            int i = Random.Range(0, n);

            if (!randomN.Contains(i))
            {
                randomN.Add(i);
                amount++;
            }
        } while (amount < n);

        return randomN;
    }
    bool success()
    {
        int score = 0;
        int goal = children.Count;
        foreach(int i in Score)
        {
            score += i;
        }
        //Debug.Log(score);
        if (score <= Mathf.Floor(goal / 2))
        {
            return false;
        }else
        {
            return true;
        }
    }
    private void OnDisable()
    {
        bool suceed = success();
        manage.checkforFails(suceed);

        foreach(Transform t in children)
        {
            t.position = ogPosition;
        }

        Score.Clear();
        /*foreach(int i in Score)
         {
             Debug.Log(i);
         }*/

        
    }
}
