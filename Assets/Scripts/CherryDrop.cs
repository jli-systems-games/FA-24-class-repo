using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CherryDrop : MonoBehaviour
{
    private CherryGame cherryGameManager;

    // Start is called before the first frame update
    void Start()
    {
        cherryGameManager = FindObjectOfType<CherryGame>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;  
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        if (other.gameObject.CompareTag("ice-cream")){
            cherryGameManager.cherryScore++;
        }

        else
        {
            cherryGameManager.cherryScore--;
        }

        cherryGameManager.cherriesDropped++;
    }
}
