using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    public Camera mainCamera;
    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3 (mousePos.x, mousePos.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1) && Input.GetMouseButton(3))
        {
            _gameManager.ChangeState(GameStates.Squeeze);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
