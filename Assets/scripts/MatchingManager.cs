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
            if(objectName != IDs) {
                return false;
            }else
            {
                return true;
            }
        }

    }
    public GameObject userInput;

    public EventManagers manage;
    List <Book> neededBooks = new List <Book>();
    List <Transform> dummyBooks = new List <Transform>();
    int index;
    public string ClickedButtonName;
    GameObject[] selected;
    private GameObject _studentid;
    Vector3 OgPos;
    private void Awake()
    {
        Book d1 = new Book("D1");
        Book c1 = new Book("C1");

        neededBooks.Add(d1);
        neededBooks.Add(c1);
    }
    private void OnEnable()
    {
        index = Random.Range(0, neededBooks.Count);
        string id = neededBooks[index].IDs;
        Debug.Log(id);
        
      
       selected = GameObject.FindGameObjectsWithTag(id);       
        

        foreach (GameObject obj in selected) {
            Debug.Log(obj);
            if(obj.GetComponent<Button>() != null)
            {
                Button btt = obj.GetComponent<Button>();
                btt.interactable = true;
            }
            else
            {   _studentid = obj;
                OgPos = obj.transform.position;
                obj.transform.position = new Vector3(obj.transform.position.x + 265f, obj.transform.position.y, 0f);
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
            {
                int index = Random.Range(0, dummyBooks.Count);
                Button butn = dummyBooks[index].GetComponent<Button>();
                if(!butn.interactable)
                {
                    butn.interactable = true;
                    added = true;
                }

            } while (added == false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        bool result = neededBooks[index].compare(userInput.name);
        manage.checkforFails(result);
        _studentid.transform.position = OgPos;

    }
}
