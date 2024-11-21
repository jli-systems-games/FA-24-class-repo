using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject[] tiles;
    private GameManager _gameManager;
    public Camera mainCamera;
    private Vector3 mousePos;

    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "ground")
                {
                    foreach(GameObject tile in tiles)
                    {
                        if (hit.collider.gameObject == tile)
                        {
                            transform.position = new Vector3(tile.transform.position.x, 1.148575f, tile.transform.position.z);
                        }
                    }
                    Debug.Log("hit tiles");
                    Debug.Log(hit.point);
                }
            }
        }
    }

    public void RotateLeft()
    {
        transform.Rotate(0, -90,0);
    }

    public void RotateRight()
    {
        transform.Rotate(0, 90, 0);
    }
}
