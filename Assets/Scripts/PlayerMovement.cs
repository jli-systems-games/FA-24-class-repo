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

    public GameObject currentTile;
    public TileDirections tileDirections;

    [SerializeField] private GameObject forwardTile;
    [SerializeField] private GameObject backTile;
    [SerializeField] private GameObject leftTile;
    [SerializeField] private GameObject rightTile;

    public PlayerDirection facing;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        Invoke("SetTiles",.5f);
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
        tileDirections = currentTile.GetComponent<TileDirections>();
        //set the tiles somewhere here relative to the player directions & call this every time they move or rotate
        if (facing == PlayerDirection.North)
        {
            if (tileDirections.northTile != null) { forwardTile = tileDirections.northTile; }
            else { forwardTile = null; }
            
            if (tileDirections.southTile != null) { backTile = tileDirections.southTile; }
            else {  backTile = null; }
            
            if (tileDirections.eastTile != null) { rightTile = tileDirections.eastTile; }
            else {  rightTile = null; }
            
            if (tileDirections.westTile != null) { leftTile = tileDirections.westTile; }
            else {  leftTile = null; }
        }

        else if (facing == PlayerDirection.East)
        {
            if (tileDirections.eastTile != null) { forwardTile = tileDirections.eastTile; }
            else { forwardTile = null; }
            
            if (tileDirections.westTile != null) { backTile = tileDirections.westTile; }
            else { backTile = null; }
            
            if (tileDirections.southTile != null) { rightTile = tileDirections.southTile; }
            else { rightTile = null; }
            
            if (tileDirections.northTile != null) { leftTile = tileDirections.northTile; }
            else { leftTile = null; }
        }

        else if (facing == PlayerDirection.South)
        {
            if (tileDirections.southTile != null) { forwardTile = tileDirections.southTile; }
            else { forwardTile = null; }
            
            if (tileDirections.northTile != null) { backTile = tileDirections.northTile; }
            else { backTile = null; }
            
            if (tileDirections.westTile != null) { rightTile = tileDirections.westTile; }
            else { rightTile = null; }
            
            if (tileDirections.eastTile != null) { leftTile = tileDirections.eastTile; }
            else { leftTile = null; }
        }

        else if(facing == PlayerDirection.West)
        {
            if (tileDirections.westTile != null) { forwardTile = tileDirections.westTile; }
            else { forwardTile = null; }
            
            if (tileDirections.eastTile != null) { backTile = tileDirections.eastTile; }
            else { backTile = null; }
            
            if (tileDirections.northTile != null) { rightTile = tileDirections.northTile; }
            else { rightTile = null; }
            
            if (tileDirections.southTile != null) { leftTile = tileDirections.southTile; }
            else { leftTile = null; }
        }
    }

    #region Movement
    public void MoveForward()
    {
        if (forwardTile != null)
        {
            currentTile = forwardTile;
            transform.position = new Vector3(currentTile.transform.position.x, 1.148575f, currentTile.transform.position.z);
            SetTiles();
        }
    }
    public void MoveBack()
    {
        if (backTile != null)
        {
            currentTile = backTile;
            transform.position = new Vector3(currentTile.transform.position.x, 1.148575f, currentTile.transform.position.z);
            SetTiles();
        }
    }
    public void MoveLeft()
    {
        if (leftTile != null)
        {
            currentTile = leftTile;
            transform.position = new Vector3(currentTile.transform.position.x, 1.148575f, currentTile.transform.position.z);
            SetTiles();
        }
    }

    public void MoveRight()
    {
        if (rightTile != null)
        {
            currentTile = rightTile;
            transform.position = new Vector3(currentTile.transform.position.x, 1.148575f, currentTile.transform.position.z);
            SetTiles();
        }
    }
    #endregion

    #region Rotations
    public void RotateNorth()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        facing = PlayerDirection.North;
        SetTiles();

    }

    public void RotateSouth()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        facing = PlayerDirection.South;
        SetTiles();

    }
    public void RotateWest()
    {
        transform.eulerAngles = new Vector3(0,270,0);
        facing = PlayerDirection.West;
        SetTiles();

    }

    public void RotateEast()
    {
        transform.eulerAngles = new Vector3(0, 90, 0);
        facing = PlayerDirection.East;
        SetTiles();

    }
    #endregion

}
