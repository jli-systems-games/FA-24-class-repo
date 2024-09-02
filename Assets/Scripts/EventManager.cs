using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;




public class EventManager : MonoBehaviour
{
    IEnumerator manager;
    public float timePassed = 15f;
    public GameObject Pong;
    List<GameObject> balls= new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        manager = determineEvents(timePassed);
        StartCoroutine(manager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator determineEvents(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            randomEvents();
        }
    }

    void randomEvents()
    {
        int numb = Random.Range(0, 3);
        bool condition = false;

        switch (numb)
        {
            case 1:
                condition = true; 
                
                TrueorFalse(condition,balls);
                //Debug.Log("true & false"); 
                //Debug.Log(numb);
                break;
            case 2:
                Debug.Log("Oh no, a stranger!");
                break;
            case 3:
                Debug.Log("wow a new Dimension");
                break;
        }
    }

    void TrueorFalse(bool events, List<GameObject> dummies)
    {
       
        GameObject wrongBall;
        int leng = dummies.Count;
       
      
        if(leng < 3)
        {
            wrongBall = Instantiate(Pong, Pong.transform.position, Quaternion.identity);
            wrongBall.tag = "Wrong";
            dummies.Add(wrongBall);
          
            if(leng >= 3)
            {
                return;
            }
           
        }

        Debug.Log(events);

        /*if(events != 1 && leng >= 1)
        {
            foreach(GameObject i in dummies)
            {
                Destroy(i);

            }
            
        }*/
    }
}
