using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    public GameObject[] AllPieces;
    int OIL = 1;

    // Start is called before the first frame update
    void Start()
    {
        
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

        void CheckCompletion()
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

            if (allInPlace)
            {
                Debug.Log("Completed");
            }
        }
    }
}
