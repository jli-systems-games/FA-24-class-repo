using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObj : MonoBehaviour
{
    public float rotationSpeed = 200f; // Rotation speed for smoothness
    private bool isRotating = false; // To prevent overlapping rotations
    public Collider2D objectCollider;

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world space
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse is within the collider bounds
            if (objectCollider.OverlapPoint(mousePosition) && !isRotating)
            {
                StartCoroutine(RotateObject(90f));
            }
        }
    }

    private System.Collections.IEnumerator RotateObject(float angle)
    {
        isRotating = true;

        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + angle;
        float currentRotation = startRotation;

        while (Mathf.Abs(currentRotation - endRotation) > 0.1f)
        {
            // Smooth rotation
            currentRotation = Mathf.MoveTowards(currentRotation, endRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            yield return null;
        }

        // Snap to the final rotation
        transform.rotation = Quaternion.Euler(0, 0, endRotation);
        isRotating = false;
    }
}
    // public Collider2D objectCollider;
    // public GameObject transformObj;

    // void Update()
    // {
    //     Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //     if (objectCollider.OverlapPoint(mousePosition) && Input.GetMouseButtonDown(0))
    //     {
    //         transformObj.transform.rotation * Quaternion.Euler(0, 0, 90);
    //     }
    // }
