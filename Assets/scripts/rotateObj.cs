using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObj : MonoBehaviour
{
    public float rotationSpeed = 400f;
    private bool isRotating = false;
    public Collider2D objectCollider;

    public GameObject startButton;

    void Start()
    {
        startButton = GameObject.FindWithTag("startB");
    }
    
    void Update()
    {
        if (startButton.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                if (objectCollider.OverlapPoint(mousePosition) && !isRotating)
                {
                    StartCoroutine(RotateObject(90f));
                }
            }
        }

    }

    private IEnumerator RotateObject(float angle)
    {
        isRotating = true;

        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + angle;
        float currentRotation = startRotation;

        while (Mathf.Abs(currentRotation - endRotation) > 0.1f)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, endRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, endRotation);
        isRotating = false;

    }
}
