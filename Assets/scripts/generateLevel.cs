using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateLevel : MonoBehaviour
{
    public float platform;

    public GameObject background;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = background.gameObject.transform.position;

        pos.x += -1 * Time.deltaTime;
    
        if(pos.x < -28)
        {
            pos.x = 15;
        }
    
    }
}
