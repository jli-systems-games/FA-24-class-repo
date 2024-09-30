using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpDrop : MonoBehaviour
{


    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;

    float uiDistance = 500f;


    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * uiDistance, Color.red, 1f);

        if (Input.GetMouseButtonDown(0))
        {
            if (objectGrabbable == null)
            {

                // Not carrying an object, try to grab
                float pickUpDistance = 4f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
                float uiDistance = 500f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit uiRaycastHit, uiDistance, pickUpLayerMask))
                {
                    Debug.Log("Raycast hit: " + uiRaycastHit.transform.name); // Log the object hit by the raycast

                    if (uiRaycastHit.transform.TryGetComponent(out Button uiButton)) // assuming you want to interact with a Button
                    {
                        uiButton.onClick.Invoke(); // Simulate a button click
                        Debug.Log("click");
                    }
                }
                //else
                //{
                //    // Currently carrying something, drop
                //    objectGrabbable.Drop();
                //    objectGrabbable = null;
                //}
            }
            else if (objectGrabbable != null)
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
                return; // Exit early since we've dropped the object
            }
        }
    }
}