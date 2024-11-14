using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  
    public float followSpeed = 5f;  

    private float initialZ;  

    void Start()
    {
        
        if (target != null)
        {
            initialZ = transform.position.z;
        }
        else
        {
            Debug.LogWarning("CameraFollow Empty");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, initialZ);

            
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
