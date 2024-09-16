using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    public GameObject[] AllPieces;
    int OIL = 1;

    public GameObject win;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        win.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast
            (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform.CompareTag("puzzle"))
            {
                if (!hit.transform.GetComponent<PuzzleBlock>().InRightPosition)
                {
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<PuzzleBlock>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }
                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedPiece != null)
            {
                SelectedPiece.GetComponent<PuzzleBlock>().Selected = false;
                SelectedPiece = null;

                CheckCompletion();
            }   
           
        }

        if(SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x,MousePoint.y,0);
        }

    }
    public void CheckCompletion()
    {
            bool allInPlace = true;
            foreach (GameObject piece in AllPieces)
            {
                PuzzleBlock puzzleBlock = piece.GetComponent<PuzzleBlock>();
                if (!puzzleBlock.InRightPosition)
                {
                    allInPlace = false;
                    break;
                }
            }

            Debug.Log(allInPlace);

            if (allInPlace)
            {
                Debug.Log("Completed");
                StartCoroutine(ShowWin());
            }
    }


    

    IEnumerator ShowWin()
    {
        win.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //_gameManager.ChangeState(GameState.Transition)
        //send to win scene, then be able to go to main menu
        //have a script in the win scene that stops all coroutines on start
        //have some stuff to reset gamemanager

        SceneManager.LoadScene("End");

    }
}
