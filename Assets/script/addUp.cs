using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addUp : MonoBehaviour
{

    public bool isPlayer1Cost;
    public bool isLeftSide;
    public GameObject LeftHand;
    public GameObject RightHand;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball Priced...");
            GameObject.Find("GameManager").GetComponent<GameManager>().BallPriced();

            if (!isLeftSide)
            {
                LeftHand.SetActive(true);
                RightHand.SetActive(false);
            }
            else
            {
                RightHand.SetActive(true);
                LeftHand.SetActive(false);
            }
        }
   
    }



    // Start is called before the first frame update
    void Start()
    {
        LeftHand.SetActive(false);
        RightHand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
