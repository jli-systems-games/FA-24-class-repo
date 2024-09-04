using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addUp : MonoBehaviour
{
   // private bool isPriced = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") /*&& gameObject.CompareTag("Trigger") && !isPriced*/)
        {
            Debug.Log("Ball Priced...");
            GameObject.Find("GameManager").GetComponent<GameManager>().BallPriced();
           // isPriced = true;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && gameObject.CompareTag("Trigger"))
        {
            isPriced = false;
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
