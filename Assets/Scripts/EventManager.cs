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
    public Players p1;
    public player2 p2;
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
        while (!p1.gameEnd || !p2.gameEnd)
        {
            yield return new WaitForSeconds(time);
            randomEvents();
        }
    }

    void randomEvents()
    {
        int numb = Random.Range(0, 3);

        //Debug.Log(balls.Capacity);
        switch (numb)
        {
            case 0:
                Debug.Log("wow a new Dimension");
                break;
            case 1:
                
                TrueorFalse(balls);
                //Debug.Log("true & false"); 
                //Debug.Log(numb);
                break;
            case 2:
                Debug.Log("Oh no, a stranger!");
                break;
          
        }

        //Debug.Log("numb:" + numb);
        resetEvents(numb);
    }

    void TrueorFalse(List<GameObject> dummies)
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
        

      
    }

    void resetEvents(int n)
    {
        int len = balls.Count;
        if(len >= 1 && n != 1)
        {
            foreach(GameObject x in balls)
            {
                Destroy(x);
            }
        }
    }
}
