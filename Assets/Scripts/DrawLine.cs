using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DrawLine : MonoBehaviour
{
    Vector2 moveVector;
    Vector3 prevDirct = Vector3.zero;
   
    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public GameObject hitBox;
    public TextMeshPro energyCount;
    private List<LineRenderer> lineEndPoints = new List<LineRenderer>();
    bool isdeleting;
    LineRenderer _LR;
   
    void Start()
    {

      
        InputManager.RemoveLine += Deleteline;
        InputManager.RenderLine += MoveLine;
    }
    

    void MoveLine(Vector3 dir, Vector3 n)
    {   
       if(dir != prevDirct) 
       {
            GameObject obj = gameObject; 
            if (gameObject.TryGetComponent<LineRenderer>(out LineRenderer l))
            {
                obj = new GameObject("newLine" + lineEndPoints.Count);
                    
            }
            LineRenderer lr= obj.AddComponent<LineRenderer>();
            lr.startWidth = startWidth;
            lr.endWidth = endWidth;
            Vector3[] tempPos = InputManager.points.ToArray();

            //as stack, the peek value wiil always be at [0] when converted to array;

            lr.SetPosition(0, tempPos[0]);
            //Debug.Log(tempPos[0]);
            lr.SetPosition(1, n);
            
            lineEndPoints.Add(lr);
        }
        else
        {
            if(lineEndPoints.Count > 0)
            {
                lineEndPoints[lineEndPoints.Count - 1].SetPosition(1, n);
            }
        }
        prevDirct = dir;
       
      
       
    }

    void Deleteline(Vector3 n, Vector3 d)
    {

        //Find the last added LineRednerer;
        Debug.Log("deleting");
        isdeleting = true;
        if(lineEndPoints.Count > 0)
        {   
            LineRenderer lr = lineEndPoints[lineEndPoints.Count - 1];

            //Retract the position[1] until n matches the position[0];

             if(n == lr.GetPosition(0))
            {
                //Debug.Log("lasy" + lr.GetPosition(0));
                
                Destroy(lr);
                 lineEndPoints.Remove(lr);
                //get the origin point from the last renderer before the deleted one;
                //to calculate the new prevDirct;
                Vector3 lastDirct = Vector3.zero;
                if(lineEndPoints.Count > 0)
                {
                    lastDirct = n - lineEndPoints[lineEndPoints.Count - 1].GetPosition(0);
                }
                
                lastDirct.Normalize();
                 
                prevDirct = lastDirct;
                 
             }
            else
            {
               lr.SetPosition(1, n);
               prevDirct = d;
            }
            
        }
        

        
    }
    private void OnDestroy()
    {
        InputManager.RemoveLine -= Deleteline;
        InputManager.RenderLine -= MoveLine;
    }
}
