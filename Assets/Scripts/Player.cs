using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    public Camera mainCamera;
    private Vector3 mousePos;
    private bool mouseMoving;

    public bool moveable;

    public bool spokeToManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        spokeToManager = false;
        moveable = true;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        if (Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) 
        {
            mouseMoving = true;
        }
        else
        {
            mouseMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!spokeToManager)
        {
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                _gameManager.ChangeState(GameStates.Squeeze);
            }

            else if (Input.GetMouseButton(0) && mouseMoving && !Input.GetMouseButton(1))
            {
                _gameManager.ChangeState(GameStates.Shake);
            }

            else if (Input.GetMouseButtonDown(2))
            {
                _gameManager.ChangeState(GameStates.Smash);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!spokeToManager)
        {
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                _gameManager.ChangeState(GameStates.Squeeze);
            }

            else if (Input.GetMouseButton(0) && mouseMoving && !Input.GetMouseButton(1))
            {
                _gameManager.ChangeState(GameStates.Shake);
            }

            else if (Input.GetMouseButtonDown(2))
            {
                _gameManager.ChangeState(GameStates.Smash);
            }
        }
    }
}
