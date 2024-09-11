using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PuzzleBlock : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;

    // Start is called before the first frame update
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(4, 7), Random.Range(3, -3));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                if(InRightPosition == false)
                {
                    transform.position = RightPosition;
                    InRightPosition = true;
                    Debug.Log("InPlace");
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
                
            }
            
        }
    }
}
