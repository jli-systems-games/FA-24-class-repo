using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    Vector2 moveVector;
    Vector3 endPos;
    public LayerMask block;
    float step = 1;
    public int Energy = 10;
    public int MaxEnergy = 10;
    public TextMeshPro energyCount;
    int prevEnergy = 0;
    bool readjusting = false;
    GameObject hitComponent = null;
    void Start()
    {
        endPos = transform.parent.position;
        transform.position = transform.parent.localPosition;
        InputManager.points.Push(endPos);
        GameManager.loadNextLvl += resetStat;
    }
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.started) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void GetDirection(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        Vector3 nextPoint = transform.position;
        if (context.started)
        {
            //calculate the new position and the direction it will go;
            Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
            
            Ray ray = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction, Color.cyan, Mathf.Infinity);

            //detecting whether it has hit a wall or not;
            if (!Physics.Raycast(ray, out RaycastHit hit, step, block))
            {   
                nextPoint = CalculateDirct(direction);
                
            }
            else if(Energy > 0)
            {   
               
                if (hit.collider.CompareTag("repeater")) nextPoint = Redirect(hit);
                readjusting = true;
                //Debug.Log("n" + nextPoint);
                
            }

            
            
           if (nextPoint == InputManager.points.Peek() &&InputManager.points.Count > 0) 
            {   
                if(InputManager.points.Count > 1)
                    InputManager.points.Pop();

                
                if (Energy < MaxEnergy && !readjusting) Energy++;

                endPos = nextPoint;
                transform.position = nextPoint;
                InputManager.DelLine(nextPoint,direction); 
            }
            else if(Energy > 0 && !InputManager.points.Contains(nextPoint) && nextPoint != endPos)
            {
                
                //Debug.Log("Added p" + endPos);
                InputManager.points.Push(endPos);

                 if (Energy > 0 && !readjusting) Energy--;

                endPos = nextPoint;
                transform.position = nextPoint;
                InputManager.DrawLine(direction,nextPoint);
            }
            
            readjusting = false;
            energyCount.text = Energy.ToString();
        }
    }

    Vector3 CalculateDirct(Vector3 dirct)
    {
        return endPos + dirct * step;
    }

    void resetStat()
    {
        readjusting = true;
    }

   Vector3 Redirect(RaycastHit _hit)
    {   
        Vector3 newLoc = transform.position;
        
        readjusting = true;
        //determine where is it coming from; 
       if(Mathf.Abs(moveVector.x) == _hit.transform.up.x)
        {
            Debug.Log("move:" + moveVector.x);
            Vector3 d = new Vector3(moveVector.x, 0, moveVector.y);
            newLoc = _hit.transform.position + d * step;

            //recalculate the energy level;
            if(_hit.transform.gameObject != hitComponent && d == _hit.transform.up)
            {
                prevEnergy = Energy;

                //grabbing the stats;
                if(_hit.collider.TryGetComponent<RepeaterSpec>(out RepeaterSpec repeat))
                {
                    Energy = repeat.EnergyOutput(Energy, MaxEnergy);
                }
                else
                {   
                    ComponentBase comp = _hit.collider.GetComponent<ComponentBase>();
                    Energy = comp.EnergyOutput(Energy);

                }
                
                hitComponent = _hit.transform.gameObject;
            }
            else if(_hit.transform.gameObject == hitComponent && d == - _hit.transform.up)
            {
                Energy = prevEnergy;
                prevEnergy = 0;
                hitComponent = null;
            }

        }

       return newLoc;
        
    }

}
