using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addUp : MonoBehaviour
{

    public bool isPlayer1Cost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball Priced...");
            GameObject.Find("GameManager").GetComponent<GameManager>().BallPriced();
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
