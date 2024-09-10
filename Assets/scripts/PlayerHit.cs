using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public RaycastHit hit;
    Ray ray;

    public bool isLeftMouseClick;
    public bool rayCastSuccessful;
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
            if (Physics.Raycast(ray, out hit, 5f))
            {
                Debug.Log("Hit");
                rayCastSuccessful = true;
                hit.transform.position = new Vector3(transform.position.x - 15f, transform.position.y, 0);
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
            if (Physics.Raycast(ray, out hit, 5f))
            {
                rayCastSuccessful = true;
                hit.transform.position = new Vector3(transform.position.x - 15f, transform.position.y, 0);
            }
            else
            {
                rayCastSuccessful = false;
            }
        }
    }
}
