using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");     

        
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
