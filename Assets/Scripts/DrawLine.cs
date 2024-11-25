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
    Vector3 endPos;
   
    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public LayerMask block;
    public GameObject hitBox;
    public TextMeshPro energyCount;
    private List<Vector3> lineEndPoints = new List<Vector3>();
    int pointCount = 0;
    LineRenderer _LR;
    public float lineLen = 3;
    void Start()
    {
        endPos = transform.position;
        _LR = GetComponent<LineRenderer>();
        _LR.startWidth = 0f;
        _LR.endWidth = 0f;

        //set the start position as the begin dot;
        lineEndPoints.Add(transform.position);
    }
    

    void MoveLine(Vector3 pos)
    {
        Vector3 newPos = pos;

        //Debug.Log(newPos);
        if (lineEndPoints.Contains(newPos))
        {
            int index = lineEndPoints.IndexOf(newPos);
            //Debug.Log("Points:" + lineEndPoints[index] + "index" + index);
            if (index == lineEndPoints.Count - 2)
            {
                RemoveLine();
                endPos = newPos;
                hitBox.transform.position = newPos;
                
            }

        }
        else if (!lineEndPoints.Contains(newPos) )
        {
            lineEndPoints.Add(newPos);
            //endPos = newPos;

            AddLine();
            endPos = newPos;
            hitBox.transform.position = newPos;

           
        }
       
    }

    void AddLine()
    {
        _LR.startWidth = startWidth;
        _LR.endWidth = endWidth;
        _LR.positionCount = lineEndPoints.Count;
        for(int i = pointCount; i < lineEndPoints.Count; i++)
        {
            _LR.SetPosition(i, lineEndPoints[i]);
        }
        pointCount = lineEndPoints.Count;

    }
    void RemoveLine()
    {   
        
        //Debug.Log("point to remove" + lineEndPoints[lineEndPoints.Count -1]);
        lineEndPoints.RemoveAt(lineEndPoints.Count - 1);
        Vector3[] lines = lineEndPoints.ToArray();
        _LR.positionCount = lines.Length;
        _LR.SetPositions(lines);
        pointCount = lines.Length;
    }
}
