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
    float step = 2;
    public int Energy = 10;
    public int MaxEnergy = 10;
    public TextMeshPro energyCount;
    int prevEnergy = 0;

    void Start()
    {
        endPos = transform.parent.position;
        transform.position = transform.parent.localPosition;
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
            if (!Physics.Raycast(ray, out RaycastHit hit, 2f, block) && Energy >0)
            {   
                nextPoint = CalculateDirct(direction);

            }
            else
            {
                if (hit.collider.CompareTag("repeater")) nextPoint = Redirect(hit);

            }

            transform.position = nextPoint;
            endPos = nextPoint;
            Energy--;
            energyCount.text = Energy.ToString();
        }
    }

    Vector3 CalculateDirct(Vector3 dirct)
    {
        return endPos + dirct * step;
    }

   Vector3 Redirect(RaycastHit _hit)
    {   
        Vector3 newLoc = transform.position;

        //determine where is it coming from; 
       if(Mathf.Abs(moveVector.x) == _hit.transform.up.x)
        {
            Debug.Log("move:" + Mathf.Abs(moveVector.x));
            Vector3 d = new Vector3(moveVector.x, 0, moveVector.y);
            newLoc = _hit.transform.position + d * step;

            //recalculate the energy level;
            if(prevEnergy == 0)
            {
                prevEnergy = Energy;

                //grabbing the stats;
                ComponentBase comp = _hit.collider.GetComponent<ComponentBase>();
                Energy = comp.EnergyOutput(Energy);
            }
            else
            {
                Energy = prevEnergy;
                prevEnergy = 0;
            }

        }

       return newLoc;
        
    }

}
