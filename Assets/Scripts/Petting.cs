using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petting : MonoBehaviour
{
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        transform.position = new Vector2(mousePos.x, mousePos.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _gameManager.BeginPetting();
        Debug.Log("triggered");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _gameManager.IncrementPets();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _gameManager.StopPetting();
    }
}
