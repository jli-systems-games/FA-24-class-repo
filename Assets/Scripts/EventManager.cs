using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class EventManager : MonoBehaviour
{
    IEnumerator manager;
    public float timePassed = 15f;
    public GameObject Pong;
    List<GameObject> balls= new List<GameObject>();
    public Players p1;
    public player2 p2;
    public GameObject enemy;
    public int numb;
    Enemy gn;
    Bounce ball;
    bool haveBug;
    public bool gameStart;
    public TMP_Text instruct;
    public GameObject nB, yB;
    void Start()
    {
        gn = enemy.GetComponent<Enemy>();
        ball = Pong.GetComponent<Bounce>();
        manager = determineEvents(timePassed);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            StartCoroutine(manager);
            nB.SetActive(false); 
            yB.SetActive(false);
            ball.start = true;
            gameStart = false;
        }
    }

    private IEnumerator determineEvents(float time)
    {
        while (!p1.gameEnd || !p2.gameEnd)
        {
            yield return new WaitForSeconds(time);
            randomEvents();
        }
        if(p1.gameEnd || p2.gameEnd) {

            StopCoroutine(manager);
        }
    }

    void randomEvents()
    {
        if (haveBug)
        {
            numb = Random.Range(1, 3);
        }
        else
        {
            numb = 1;

        }
       
        //Debug.Log(balls.Capacity);
        switch (numb)
        {
            case 1:
                
                TrueorFalse(balls);
                resetEvents(numb);
                break;
            case 2:
                Gnome();
                resetEvents(numb);
                break;

        }

        //Debug.Log("numb:" + numb);
        //resetEvents(numb);
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
    void Gnome()
    {
        enemy.SetActive(true);
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
        
        if (n != 2 || gn.isCaughtByPlayer == true)
        {
            Debug.Log("Reset");
            enemy.SetActive(false);
            //ball.Reset();
            
        }
    }

    public void noBug()
    {
        instruct.enabled = false;
        haveBug = false;
        gameStart = true;
    }

    public void yesBug()
    {
        instruct.enabled = false;
        haveBug = true;
        gameStart = true;
    }
}
