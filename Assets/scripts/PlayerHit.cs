using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MatchingManager;

public class PlayerHit : MonoBehaviour
{
    public RaycastHit hit;
    Ray ray;

    public bool isLeftMouseClick;
    public bool rayCastSuccessful;
    public GameObject holdStamp;
    public GameObject returnStamp;
    //public GameObject hitObject;
    public StampingParent Grandparent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLeftMouseClick = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 55f))
            {
                //Debug.Log("Hit");
                rayCastSuccessful = true;
                //hitObject = hit.collider.gameObject;
                AddHoldStamp();
                hit.transform.position = new Vector3(hit.transform.position.x - 10f, hit.transform.position.y, hit.transform.position.z);
            }
            else
            {
                rayCastSuccessful = false;
            }

        }
        else if(Input.GetMouseButtonDown(1))
        {
            isLeftMouseClick = false;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 55f))
            {
                rayCastSuccessful = true;
                //hitObject = hit.collider.gameObject;
                AddReturnStamp();
                hit.transform.position = new Vector3(hit.transform.position.x - 10f, hit.transform.position.y, hit.transform.position.z);
            }
            else
            {
                rayCastSuccessful = false;
            }
        }

        checkSuccess();
    }

    void checkSuccess()
    {
        if (isLeftMouseClick)
        {
            if (rayCastSuccessful && hit.collider.gameObject.tag == "Hold")
            {
                Grandparent.Score.Add(1);
            }
            else
            {
                Grandparent.Score.Add(0);
            }

        }
        else
        {

            if (rayCastSuccessful && hit.collider.gameObject.tag == "Return")
            {
                Debug.Log("correct");
                Grandparent.Score.Add(1);
            }
            else
            {
                Grandparent.Score.Add(0);
            }

        }
    }

    void AddHoldStamp()
    {
        GameObject childStamp = Instantiate(holdStamp);
        childStamp.transform.SetParent(hit.transform);
        childStamp.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.25f);
        childStamp.SetActive(true);
    }

    void AddReturnStamp()
    {
        GameObject childReturn= Instantiate(returnStamp);
        childReturn.transform.SetParent(hit.transform);
        childReturn.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 0.25f);
        childReturn.SetActive(true);
    }
}
