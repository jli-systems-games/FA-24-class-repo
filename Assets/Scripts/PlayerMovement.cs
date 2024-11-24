using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDirection //the direction the player is facing
{
    North,
    East,
    South,
    West
}

public class PlayerMovement : MonoBehaviour
{
    private GameManager _gameManager;
    public Camera mainCamera;
    private Vector3 mousePos;

    private Ray ray;
    private RaycastHit hit;

    private GameObject forwardTile;
    private GameObject backTile;
    private GameObject leftTile;
    private GameObject rightTile;

    public PlayerDirection facing;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (Input.GetMouseButtonDown(0) && GameManager.state == GameState.moveable)
        //    {
        //        if (hit.collider.tag == "ground")
        //        {
        //            foreach(GameObject tile in tiles)
        //            {
        //                if (hit.collider.gameObject == tile)
        //                {
        //                    transform.position = new Vector3(tile.transform.position.x, 1.148575f, tile.transform.position.z);
        //                }
        //            }
        //            Debug.Log("hit tiles");
        //            Debug.Log(hit.point);
        //        }
        //    }
        //}
    }

    void SetTiles()
    {
        //set the tiles somewhere here relative to the player directions & call this every time they move or rotate
    }
    #region Movement
    public void MoveForward()
    {

    }
    public void MoveBack()
    {

    }
    public void MoveLeft()
    {
    }

    public void MoveRight()
    {
    }
    #endregion

    #region Rotations
    public void RotateNorth()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        facing = PlayerDirection.North;
    }

    public void RotateSouth()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        facing = PlayerDirection.South;
    }
    public void RotateWest()
    {
        transform.eulerAngles = new Vector3(0,270,0);
        facing = PlayerDirection.West;
    }

    public void RotateEast()
    {
        transform.eulerAngles = new Vector3(0, 90, 0);
        facing = PlayerDirection.East;
    }
    #endregion

}
