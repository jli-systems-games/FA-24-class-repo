using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SlicePlaneControl : MonoBehaviour
{
    [SerializeField] PickupNSetdown pick;
    [SerializeField] LineRenderer laser;
    //[SerializeField] NavigationManager nav;
    RaycastHit hit;
    Slice sliceobj;
    public IEnumerator stopFire;
 
    bool hasSliced;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        Vector3 direction = laser.GetPosition(1) - laser.GetPosition(0);
        //Debug.DrawRay(transform.position,direction * 5, Color.green);
        if (Input.GetKey(KeyCode.E))
        {
            if (!hasSliced)
            {   
                if(Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
                {
                    
                    GameObject obj = hit.collider.gameObject;
                    
                    if (obj.TryGetComponent(out Slice _slice))
                    {
                        //StartCoroutine
                        hasSliced = true;
                        StartCoroutine(StartSlicing(_slice));
                      
                        if(obj.CompareTag("Enemy"))
                        {
                            NavigationManager npc = obj.GetComponentInParent<NavigationManager>();
                            npc.gotKilled = true;
                            
                            //StopCoroutine(npc.routineToStop);
                            npc.killHandling();
                        }
                    }
                    {
                        
                    }
                }
            }
        }
    }

    IEnumerator StartSlicing(Slice _slice)
    {
        Debug.Log("SLICING");
        _slice.ComputeSlice(Vector3.up, this.transform.position);

        yield return new WaitForSeconds(1f);

        hasSliced = false;

       
    }

    
}
