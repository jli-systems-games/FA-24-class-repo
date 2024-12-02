using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<BuckController> controllers; 
    public float followSpeed = 5f;
    public float fixedYPosition = 10f;

    private BuckController activeController; 

    private void Start()
    {
       
        controllers = new List<BuckController>(FindObjectsOfType<BuckController>());
    }

    private void Update()
    {
       
        UpdateActiveController();

        if (activeController != null)
        {
            Vector3 targetPosition = new Vector3(activeController.transform.position.x, fixedYPosition, activeController.transform.position.z - 7);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private void UpdateActiveController()
    {
       
        foreach (var controller in controllers)
        {
            if (controller.isActive)
            {
                activeController = controller;
                break;
            }
        }
    }
}
