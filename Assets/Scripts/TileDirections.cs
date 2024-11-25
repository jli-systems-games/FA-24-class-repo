using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirections : MonoBehaviour
{
    public GameObject northTile;
    public GameObject southTile;
    public GameObject eastTile;
    public GameObject westTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // need to rework this cuz its bugged af

        if (other.CompareTag("north"))
        {
            other.gameObject.transform.parent.GetComponent<TileDirections>().northTile = gameObject;
        }
        else if (other.CompareTag("south"))
        {
            other.gameObject.transform.parent.GetComponent<TileDirections>().southTile = gameObject;
        }
        else if (other.CompareTag("east"))
        {
            other.gameObject.transform.parent.GetComponent<TileDirections>().eastTile = gameObject;
        }
        else if (other.CompareTag("west"))
        {
            other.gameObject.transform.parent.GetComponent<TileDirections>().westTile = gameObject;
        }

        Debug.Log(this.gameObject.ToString() + " collides with " + other);

        // for if i end up attaching this to the triggers
        //if (direction == PlayerDirection.North)
        //{
        //    transform.parent.GetComponent<TileDirections>().northTile = other.gameObject;
        //}
        //if (direction == PlayerDirection.South)
        //{
        //    transform.parent.GetComponent<TileDirections>().southTile = other.gameObject;
        //}
        //if (direction == PlayerDirection.East)
        //{
        //    transform.parent.GetComponent<TileDirections>().eastTile = other.gameObject;
        //}
        //if (direction == PlayerDirection.West)
        //{
        //    transform.parent.GetComponent<TileDirections>().westTile = other.gameObject;
        //}
    }
}
