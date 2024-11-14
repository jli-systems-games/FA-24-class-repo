using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeBeyblades : MonoBehaviour
{
    public GameObject beyblade1Prefab;
    public GameObject beyblade2Prefab;

    public Rigidbody2D bb1RB;
    public Rigidbody2D bb2RB;

    public bool placedBB1;
    public bool placedBB2;

    public GameObject letItRipButton;
    public bool started;
    
    public Camera mainCamera; // Reference to the main camera (you can drag the camera here in the Inspector)

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && placedBB1 == false) // Check for mouse click (or use Input.touch for mobile)
        {
            placedBB1 = true;
            Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen space
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // Convert to world space

            worldPosition.z = 0; // Ensure the prefab is placed on the 2D plane (z should be 0)

            Instantiate(beyblade1Prefab, worldPosition, Quaternion.identity); // Instantiate the prefab at the world position
        }

        if (Input.GetMouseButtonDown(1) && placedBB2 == false) // Check for mouse click (or use Input.touch for mobile)
        {
            placedBB2 = true;
            Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen space
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // Convert to world space

            worldPosition.z = 0; // Ensure the prefab is placed on the 2D plane (z should be 0)

            Instantiate(beyblade2Prefab, worldPosition, Quaternion.identity); // Instantiate the prefab at the world position
        }

        if (placedBB1 == true && placedBB2 == true)
        {
            letItRipButton.gameObject.SetActive(true);
        }

        if (started == true)
        {
            letItRipButton.gameObject.SetActive(false);
        }

    }

    public void letItRip()
    {   
        started = true;
        
        GameObject beyblade1 = GameObject.FindWithTag("Beyblade1");
        GameObject beyblade2 = GameObject.FindWithTag("Beyblade2");
        
        bb1RB = beyblade1.GetComponent<Rigidbody2D>();
        bb2RB = beyblade2.GetComponent<Rigidbody2D>();

        beyblade1.GetComponent<beybladeMovement>().enabled = true;
        bb1RB.bodyType = RigidbodyType2D.Dynamic;

        beyblade2.GetComponent<beybladeMovement2>().enabled = true;
        bb2RB.bodyType = RigidbodyType2D.Dynamic;
 
    }
}

