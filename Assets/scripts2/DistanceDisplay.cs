using UnityEngine;
using TMPro;

public class DistanceDisplay : MonoBehaviour
{
    public Transform car;  
    public TextMeshProUGUI distanceText; 
    public GameObject panel; 
    public TextMeshProUGUI noChangeText;  

    private float initialX; 
    private float lastX; 
    private float timer = 0f;  
    private float thresholdTime = 5f;  
    private float movementThreshold = 0.10f; 

    private float lastDistance; 

    void Start()
    {
        if (car != null)
        {
           
            initialX = car.position.x;
            lastX = initialX;
        }
        else
        {
          
        }

        
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (car != null && distanceText != null)
        {
          
            float distance = Mathf.Abs(car.position.x - initialX);

           
            distanceText.text = distance.ToString("F2") + " m";

           
            if (Mathf.Abs(car.position.x - lastX) > movementThreshold)
            {
               
                timer = 0f;
                lastX = car.position.x;

                
                lastDistance = distance;
            }
            else
            {
               
                timer += Time.deltaTime;

               
                if (timer >= thresholdTime)
                {
                    if (panel != null)
                    {
                        panel.SetActive(true);  
                    }

                    if (distanceText != null)
                    {
                        distanceText.gameObject.SetActive(false); 
                    }

                    if (noChangeText != null)
                    {
                        noChangeText.gameObject.SetActive(true); 

                        
                        noChangeText.text = lastDistance.ToString("F2") + " m";
                    }
                }
            }
        }
    }
}
