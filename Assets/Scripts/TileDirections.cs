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
            other.gameObject.transform.parent.GetComponent<TileDirections>().northTile = this.gameObject;
        }
        if (other.CompareTag("south"))
        {
            other.gameObject.transform.parent.GetComponent<TileDirections>().southTile = this.gameObject;
        }
        if (other.CompareTag("east"))
        {
            other.gameObject.transform.parent.GetComponent<TileDirections>().eastTile = this.gameObject;
        }
        if (other.CompareTag("west"))
        {
            other.gameObject.transform.parent.GetComponent<TileDirections>().westTile = this.gameObject;
        }
    }
}
