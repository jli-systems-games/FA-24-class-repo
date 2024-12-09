using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject[] tiles;
    public GameObject currentTile;
    public TileDirections tileDirections;

    [SerializeField] private GameObject forwardTile;
    [SerializeField] private GameObject backTile;
    [SerializeField] private GameObject leftTile;
    [SerializeField] private GameObject rightTile;

    public GameObject[] rotateButtons;

    public Sprite[] compassDirections;
    public Image[] compassImages;

    public GameObject compass;

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

    }

    void PointAndClickMovement()
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
        //add compass rotations later so the player doesn't have to constantly orient themself

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

            compassImages[0].sprite = compassDirections[0];
            compassImages[1].sprite = compassDirections[1];
            compassImages[2].sprite = compassDirections[2];
            compassImages[3].sprite = compassDirections[3];
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

            compassImages[0].sprite = compassDirections[1];
            compassImages[1].sprite = compassDirections[2];
            compassImages[2].sprite = compassDirections[3];
            compassImages[3].sprite = compassDirections[0];
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

            compassImages[0].sprite = compassDirections[2];
            compassImages[1].sprite = compassDirections[3];
            compassImages[2].sprite = compassDirections[0];
            compassImages[3].sprite = compassDirections[1];
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
            compassImages[0].sprite = compassDirections[3];
            compassImages[1].sprite = compassDirections[0];
            compassImages[2].sprite = compassDirections[1];
            compassImages[3].sprite = compassDirections[2];
        }
    }

    #region Movement
    public void MoveForward()
    {
        if (forwardTile != null)
        {
            currentTile = forwardTile;
            transform.position = currentTile.transform.position;
            SetTiles();
        }
    }
    public void MoveBack()
    {
        if (backTile != null)
        {
            currentTile = backTile;
            transform.position = currentTile.transform.position;
            SetTiles();
        }
    }
    public void MoveLeft()
    {
        if (leftTile != null)
        {
            currentTile = leftTile;
            transform.position = currentTile.transform.position;
            SetTiles();
        }
    }

    public void MoveRight()
    {
        if (rightTile != null)
        {
            currentTile = rightTile;
            transform.position = currentTile.transform.position;
            SetTiles();
        }
    }
    #endregion

    #region Rotations

    // 0 - north, 1 - east, 2 - south, 3 - west
    public void RotateLeft()
    {
        transform.Rotate(0, -90, 0);
        compass.transform.Rotate(0, 0, -90);

        if(facing == PlayerDirection.North)
        {
            facing = PlayerDirection.West;
        }
        else if (facing == PlayerDirection.West)
        {
            facing = PlayerDirection.South;
        }
        else if (facing == PlayerDirection.South)
        {
            facing = PlayerDirection.East;
        }
        else if(facing == PlayerDirection.East)
        {
            facing = PlayerDirection.North;
        }

        SetTiles();
    }

    public void RotateRight()
    {
        transform.Rotate(0, 90, 0);
        compass.transform.Rotate(0, 0, 90);
        
        if(facing == PlayerDirection.North)
        {
            facing = PlayerDirection.East;
        }
        else if(facing == PlayerDirection.East)
        {
            facing = PlayerDirection.South;
        }
        else if(facing == PlayerDirection.South)
        {
            facing = PlayerDirection.West;
        }
        else if(facing == PlayerDirection.West)
        {
            facing = PlayerDirection.North;
        }

        SetTiles();
    }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ladder"))
        {
            foreach(GameObject button in rotateButtons)
            {
                button.SetActive(false);
            }

            RotateEast();
        }

        if (other.gameObject.CompareTag("tiles"))
        {
            foreach(GameObject tile in tiles)
            {
                tile.SetActive(false);
            }

            GameObject[] activeTiles = other.GetComponent<TileActivity>().TilesActive;
            GameObject[] newTiles = other.GetComponent<TileActivity>().NewlyAvailableTiles;
            Debug.Log(activeTiles);

            foreach(GameObject tile in activeTiles)
            {
                tile.SetActive(true);
            }

            if (other.GetComponent<TileActivity>().puzzle.solved && newTiles != null)
            {
                foreach(GameObject tile in newTiles)
                {
                    tile.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ladder"))
        {
            foreach(GameObject button in rotateButtons)
            {
                button.SetActive(true);
            }
        }
    }
}
