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
    public bool clothes;

    public bool dontRotate;

    AudioSource audiosource;
    public AudioClip pickUpSound;
    public List<AudioClip> placingSounds = new List<AudioClip>();

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (clothes == true)
        {
            selected = false;
        }
        else
        {
            selected = true;
        }

        if (rock == true)
        {
            transform.rotation = Quaternion.AngleAxis((Random.Range(0, 360)), Vector3.forward);

            float randomScale = Random.Range(0.005f, 0.013f);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            audiosource.clip = pickUpSound;
            audiosource.Play();
        }

        if (carrot)
        {
            audiosource.clip = pickUpSound;
            audiosource.Play();
        }
    }

    private void Update()
    {
        if (selected == true)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (carrot == true && transform.position.x < 4)
        {
            spriteRenderer.flipX = true;
        }
        else if (carrot == true && transform.position.x > 4)
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

            int randomIndex = Random.Range(0, placingSounds.Count);
            audiosource.clip = placingSounds[randomIndex];
            audiosource.Play();
        }
        else if (selected == false)
        {
            //object is not selected, select and pick up object
            selected = true;

            audiosource.clip = pickUpSound;
            audiosource.Play();


            if (dontRotate == false)
            {
                //picking up anything not a carrot, random rotation
                transform.rotation = Quaternion.AngleAxis((Random.Range(0, 360)), Vector3.forward);
            }
        }
        Debug.Log("move object");
    }
}