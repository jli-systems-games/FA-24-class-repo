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
    public GameObject[] Ids;
    public EventManagers manage;
    List <Book> neededBooks = new List <Book>();
    int index;
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
        
        List<GameObject> selected = new List<GameObject>();
        
        for (int i = 0; i < 2; i++)
        {
                selected.Add(GameObject.FindWithTag(id));       
        }

        foreach (GameObject obj in selected) {
            Debug.Log(obj);
            if(obj.GetComponent<Button>() != null)
            {
                Button btt = obj.GetComponent<Button>();
                btt.interactable = true;
            }
            else
            {
                obj.transform.position = new Vector3(-220f, obj.transform.position.y, 0f);
            }
        }
        

        if (!manage.firstpass)
        {

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
