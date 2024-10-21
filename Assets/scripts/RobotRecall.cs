using UnityEngine;

public class RobotRecall : MonoBehaviour
{
    public Transform chargingStationPoint;  
    public Transform repairStationPoint;    
    public float recallSpeed = 3.0f;         

    private RobotMovement robotMovement;
    private RobotStatus robotStatus;
    private Rigidbody rb;
    private bool isRecalling = false;
    private Transform targetRecallPoint;

    void Start()
    {
        robotMovement = GetComponent<RobotMovement>();
        robotStatus = GetComponent<RobotStatus>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!robotStatus.IsAlive())
            return;

        if (isRecalling && targetRecallPoint != null)
        {
            
            robotMovement.SetIsMoving(false);

            
            MoveTowardsRecallPoint();
        }
    }

   
    public void Recall(Transform recallPoint)
    {
        if (!robotStatus.IsAlive())
            return;

        targetRecallPoint = recallPoint;
        isRecalling = true;
    }

    private void MoveTowardsRecallPoint()
    {
        Vector3 direction = (targetRecallPoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetRecallPoint.position);

        
        rb.MovePosition(transform.position + direction * recallSpeed * Time.deltaTime);

        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        
        if (distance < 0.1f)
        {
            isRecalling = false;
            robotMovement.SetIsMoving(true); 
            targetRecallPoint = null;        
        }
    }
}
