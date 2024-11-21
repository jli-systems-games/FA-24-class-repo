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
    public int Energy = 10;
    public int MaxEnergy = 10;
    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public LayerMask block;
    public GameObject hitBox;
    public TextMeshPro energyCount;
    private List<Vector3> lineEndPoints = new List<Vector3>();
    int pointCount = 0;
    LineRenderer _LR;
    bool readjusting;
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
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.started) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void GetDirection(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        if(context.started)
        {
            //calculate the new position and the distance it will go;
            Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
            Vector3 nextPoint = CalculateDirct(direction);
            Ray ray = new Ray(hitBox.transform.position, direction);

            //Debug.DrawRay(hitBox.transform.position, direction, Color.cyan, Mathf.Infinity);
            if (!Physics.Raycast(ray, 2.5f,block) && direction != Vector3.zero)
            {
                MoveLine(nextPoint);
                

            }
            
        }
    }
    Vector3 CalculateDirct(Vector3 dirct)
    {
        Vector3 newPos = Vector3.zero;
        
        Ray _ray = new Ray(hitBox.transform.position, dirct);
        Debug.DrawRay(hitBox.transform.position, dirct * lineLen, Color.cyan, Mathf.Infinity);
        if(Physics.Raycast(_ray, out RaycastHit _hit, 1f))
        {  
            
            readjusting = true;
            Debug.Log("moving" + dirct);
            switch (_hit.collider.tag)
            {
                case "repeater":
                    
                    Vector3 fromD = _hit.transform.position - hitBox.transform.position;
                    fromD = fromD.normalized;
                    if (Mathf.Abs(fromD.x) == _hit.transform.up.x || Mathf.Abs(fromD.z) == _hit.transform.up.z)
                    {   
                        //new destination is the opposite end of the hit object;
                        Vector3 opposLoc = _hit.transform.position + fromD * lineLen;

                        //testing Objects;
                       /* GameObject ob = new GameObject("test");
                        ob.transform.position = opposLoc;*/
                       
                        newPos = opposLoc;
                        
                    }
                    else
                    {   
                        
                        //dirct = Vector3.zero;
                    }
                    ComponentBase comp = _hit.collider.GetComponent<ComponentBase>();
                    Energy = comp.EnergyOutput(Energy);
                    //Debug.Log("added" + Energy);

                    //Debug.Log("d" + dirct);
                    break;
            }
        }
        else
        {
            newPos = endPos + dirct * lineLen;
        }

        return newPos;
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
                if(Energy < MaxEnergy) Energy++;
            }

        }
        else if (!lineEndPoints.Contains(newPos) && Energy > 0)
        {
            lineEndPoints.Add(newPos);
            //endPos = newPos;

            AddLine();
            endPos = newPos;
            hitBox.transform.position = newPos;

            if(!readjusting) Energy--;
            readjusting = false;
        }
        energyCount.text = Energy.ToString();
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
