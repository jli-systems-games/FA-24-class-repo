using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Script : MonoBehaviour
{
    public bool miniGameWon;

    public Walls;


    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void Game1End()
    {
        //should check if player won or loss game??
        if (miniGameWon == true)
        {

        } else if (miniGameWon == false)
        {

        }

        //scoring system?
        //score++; lmaoo


        //reset moving parts positions..
        transform.localPosition = startPosition;
        //specify all items?
    }
}
