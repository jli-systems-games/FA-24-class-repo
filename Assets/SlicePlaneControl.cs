using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicePlaneControl : MonoBehaviour
{
    [SerializeField] PickupNSetdown pick;

    RaycastHit hit;
    Slice sliceobj;
    bool hasSliced;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!hasSliced)
            {   
                if(Physics.Raycast(transform.position, transform.forward, out hit, 100f))
                {
                    GameObject obj = hit.collider.gameObject;
                    if (obj.TryGetComponent(out Slice _slice))
                    {
                        //StartCoroutine
                        hasSliced = true;
                        StartCoroutine(StartSlicing(_slice));
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

        yield return new WaitForSeconds(2f);

        hasSliced = false;
    }
}
