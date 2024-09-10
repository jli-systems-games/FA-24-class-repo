using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManagers : MonoBehaviour
{
    public float time = 10f;
    public int fails = 0;
    public bool gameEnd;
    IEnumerator manager;
    public bool sucess;
    public LoadingPrinter printer;
    public TMP_Text endStart_text;
    

    public bool firstpass = true;
    int n = 0;
    TMP_Text suceed;
    public GameObject canvas1,canvas2,canvas3,BookGrand,plyInput,StartandEnd,sucessText;
    // Start is called before the first frame update
    void Start()
    {
        manager = manageEvents(time);
        StartCoroutine(manager);
        suceed = sucessText.GetComponent<TMP_Text>();
    }
    
    // Update is called once per frame
    void Update()
    {
       // Debug.Log(fails);
    }
    private IEnumerator manageEvents(float t)
    {  
        while (!gameEnd)
        {
            //Debug.Log("gping");
            
            determineEvents(); 
            yield return new WaitForSeconds(t);
            TranstionPhase();
            yield return new WaitForSeconds(2);

        }
        if (gameEnd)
        {
            canvas1.SetActive(false);
            canvas2.SetActive(false);
            canvas3.SetActive(false);
            StartandEnd.SetActive(true);
            suceed.text = string.Empty;
            endStart_text.text = "You got fired.";
        }
    }

    void determineEvents()
    {
       
        
        if(fails <= 2)
        {
           
            if(firstpass)
            {
               
                
                if(n >= 3)
                {
                    firstpass = false;
                    //return;
                }else
                {
                    n++;
                }
            }
            else
            {   //ensure that there is not repeating numbers back to back;
                int nextNumb;
                do
                {
                    nextNumb = Random.Range(1, 4);
                } while (n == nextNumb);

                n = nextNumb;
                
            }

            //Debug.Log(n);
            switch (n)
                {
                    case 1:
                        Event1();
                   
                    break;
                    case 2:
                        Event2();
                    ;
                    break;
                    case 3:
                        Event3();
                    
                    break;
                }
        }
        else
        {
            //a boolean that will be controlled by the fail counters, which are updated by scripts on event objects.
            gameEnd = true;
        }
    }

    void Event1()
    {
        canvas1.SetActive(true);


        //turn off other events if they are active.
        if(canvas2.activeSelf)
        {
            canvas2.SetActive(false);
            BookGrand.SetActive(false);
            plyInput.SetActive(false);
        }
        else if (canvas3.activeSelf)
        {
            canvas3.SetActive(false);
        }
        else if (StartandEnd.activeSelf)
        {
            StartandEnd.SetActive(false);
        };

    }
    void Event2()
    {
        canvas2.SetActive(true);

        BookGrand.SetActive(true);
        plyInput.SetActive(true);

        if (canvas1.activeSelf)
        {
            canvas1.SetActive(false);
        }
        else if (canvas3.activeSelf)
        {
            canvas3.SetActive(false);
        }else if (StartandEnd.activeSelf)
        {
            StartandEnd.SetActive(false);
        };
    }
    void Event3()
    {
        canvas3.SetActive(true);



        if (canvas1.activeSelf)
        {
            canvas1.SetActive(false);
        }
        else if (canvas2.activeSelf)
        {
            canvas2.SetActive(false);
            BookGrand.SetActive(false);
            plyInput.SetActive(false);
        }
        else if (StartandEnd.activeSelf)
        {
            StartandEnd.SetActive(false);
        };
    }

    public void checkforFails(bool b)
    {
        
        
        if (b == false)
        {
            fails++;
            sucessText.SetActive(true);
            suceed.text = "://";
            Debug.Log("://");
        }
        else
        {
            sucessText.SetActive(true);
            suceed.text = "Yay";
            
        }
    }
    public void TranstionPhase()
    {
        StartandEnd.SetActive(true);

        if (canvas1.activeSelf)
        {
            canvas1.SetActive(false);
        }
        else if (canvas2.activeSelf)
        {
            canvas2.SetActive(false);
            BookGrand.SetActive(false);
            plyInput.SetActive(false);
        }
        else if (canvas3.activeSelf)
        {
            canvas3.SetActive(false);
        }
    }
}
