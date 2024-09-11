using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragObject : MonoBehaviour
{
    Vector2 difference = Vector2.zero;

    public float timeLimit = 3f;
    private bool isDragging = false;
    private float timer;
    private bool isGameOver = false; // bool to show if game is over

    public GameObject mouse;
    public TextMeshProUGUI victory;
    public TextMeshProUGUI failed;

    private void Start()
    {
        timer = timeLimit;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Fail();
            }
        }
    }

    private void OnMouseDown()
    {
        if (isGameOver) return;

        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isGameOver) return;

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private void OnMouseUp()
    {
        if (isGameOver) return;

        isDragging = false;

        if (GetComponent<Collider2D>().IsTouching(mouse.GetComponent<Collider2D>()))
        {
            Success();
        }
        else
        {
            Fail();
        }
    }

    private void Success()
    {
        victory.gameObject.SetActive(true);
        failed.gameObject.SetActive(false);
        isGameOver = true;
    }

    private void Fail()
    {
        victory.gameObject.SetActive(false);
        failed.gameObject.SetActive(true);
        isGameOver = true;
    }
}
