using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDrag : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    //public GameObject rocks;

    public bool selected;

    private SpriteRenderer spriteRenderer;

    public bool rock;
    public bool carrot;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        selected = true;

        if (rock == true)
        {
            transform.rotation = Quaternion.AngleAxis((Random.Range(0, 360)), Vector3.forward);

            float randomScale = Random.Range(0.3f, 0.5f);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }

    private void Update()
    {
        if (selected == true)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (carrot == true && transform.position.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (carrot == true && transform.position.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    //https://discussions.unity.com/t/drag-gameobject-with-mouse/1798
    void OnMouseDown()
    {
        if (selected == true)
        {
            //object is selected, deselect and place object
            selected = false;
        }
        else if (selected == false)
        {
            //object is not selected, select and pick up object
            selected = true;

            if (carrot == false)
            {
                //picking up anything not a carrot, random rotation
                transform.rotation = Quaternion.AngleAxis((Random.Range(0, 360)), Vector3.forward);
            }
        }
        Debug.Log("move object");
    }
}

