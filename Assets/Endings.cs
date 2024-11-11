using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endings : MonoBehaviour
{

    public GameObject player;

    public GameObject player2;


    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(true);
        player2.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > 660f)
        {
            player.SetActive(false);
            player2.SetActive(true);
        }
        else
        {

            player.SetActive(true);
            player2.SetActive(false);
        }

        if (player.transform.position.x > 660f)
        {

        }

        if (player.transform.position.x < -660f)
        {

        }
    }
}
