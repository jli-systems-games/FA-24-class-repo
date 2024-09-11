using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MatchingManager : MonoBehaviour
{   
    public class Book
    {
        public string IDs;

        public Book(string name)
        {
            this.IDs = name;
        }
        public bool compare(string objectName)
        {   
            if(objectName != null)
            {
                if(objectName != IDs) {
                    return false;
                }else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
           
        }

    }
    public string userInput;

    public EventManagers manage;
    List <Book> neededBooks = new List <Book>();
    List <Transform> dummyBooks = new List <Transform>();
    int index,activebtn;
    public string ClickedButtonName;
    GameObject[] selected;
    private GameObject _studentid;
    public RectTransform canvas;
    Vector3 OgPos;
    float x, y;
    private void Awake()
    {
        Book d1 = new Book("D1");
        Book c1 = new Book("C1");
        Book e2 = new Book("E2");
        Book b2 = new Book("B2");

        neededBooks.Add(d1);
        neededBooks.Add(c1);
        neededBooks.Add(e2);
        neededBooks.Add(b2);
    }
    private void OnEnable()
    {
        
        index = Mathf.FloorToInt(Random.Range(0, neededBooks.Count));
        
        string id = neededBooks[index].IDs;
        userInput = string.Empty;
        
      
        selected = GameObject.FindGameObjectsWithTag(id);       
        

        foreach (GameObject obj in selected) {
            
            if(obj.GetComponent<Button>() != null)
            {
                Button btt = obj.GetComponent<Button>();
                btt.interactable = true;
            }
            else
            {   _studentid = obj;
                OgPos = obj.transform.position;
               
                obj.transform.position = new Vector3(obj.transform.position.x + 5f, obj.transform.position.y, 0f);
                //Debug.Log("It moved");
            }
        }

        bool added = false;
        if (manage.firstpass)
        {
            foreach(Transform t in transform)
            {
                dummyBooks.Add(t);  
            }
        }
        else
        {
            do
            {   if(activebtn != dummyBooks.Count)
                {
                    int index = Random.Range(0, dummyBooks.Count);
                    Button butn = dummyBooks[index].GetComponent<Button>();
                    if(!butn.interactable)
                    {
                        butn.interactable = true;
                        added = true;
                    }
                }
                else
                {
                    added = true;
                }
                

            } while (added == false);
        }
    }
    void Start()
    {
        x = canvas.position.x;
        y = canvas.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    { bool result; 
        if(userInput != null)
        {
            result = neededBooks[index].compare(userInput);
        }else
        {
            result = false;
        }
       
        manage.checkforFails(result);
        _studentid.transform.position = OgPos;

        foreach (Transform b in dummyBooks)
        {
            if (b.GetComponent<Button>().interactable)
            {
                activebtn++;
            }
        }

    }
}
