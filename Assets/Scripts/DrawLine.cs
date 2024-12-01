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
    int pointCount = 0;
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
                obj = new GameObject("newLine");
                    
            }
            LineRenderer lr= obj.AddComponent<LineRenderer>();
            lr.startWidth = startWidth;
            lr.endWidth = endWidth;
            Vector3[] tempPos = InputManager.points.ToArray();
            lr.SetPosition(0, tempPos[0]);
            lr.SetPosition(1, n);
            prevDirct = dir;
            lineEndPoints.Add(lr);
        }
        else
        {
            if(lineEndPoints.Count > 0)
            {
                lineEndPoints[lineEndPoints.Count - 1].SetPosition(1, n);
            }
        }

       
      
       
    }

    void Deleteline(Vector3 n, Vector3 d)
    {

        //Find the last added LineRednerer;
        Debug.Log("deleting");
        if(lineEndPoints.Count > 0)
        {
            LineRenderer lr = lineEndPoints[lineEndPoints.Count - 1];
            //Retract the position[1] until n matches the position[0];

             if(n == lr.GetPosition(0))
            {
                 //Debug.Log("lasy" + lr.GetPosition(0));
                 //Debug.Log("next" + n);
                 lineEndPoints.Remove(lr);
                 Destroy(lr);
             }
            else
            {
               lr.SetPosition(1, n);
            }
            prevDirct = d;
        }
        

        
    }
    private void OnDestroy()
    {
        InputManager.RemoveLine -= Deleteline;
        InputManager.RenderLine -= MoveLine;
    }
}
