using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDrop : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float pickUpDistance = 2f;
            //if (Physics.Raycast(playerCameraTransform, playerCameraTransform.forward, (out RaycastHit raycastHit, pickupDistance, pickUpLayerMask)))
            {

            }
              
        }
    }
}
