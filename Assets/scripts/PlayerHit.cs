using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public RaycastHit hit;
    Ray ray;

    public bool isLeftMouseClick;
    public bool rayCastSuccessful;
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
                hit.transform.position = new Vector3(transform.position.x - 12f, transform.position.y, 0);
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
                hit.transform.position = new Vector3(transform.position.x - 12f, transform.position.y, 0);
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
}
