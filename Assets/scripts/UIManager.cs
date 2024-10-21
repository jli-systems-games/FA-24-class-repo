using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections;
using UnityEngine.SceneManagement; 

public class UIManager : MonoBehaviour
{
    public Slider batterySlider;
    public Slider cleanlinessSlider;
    public Slider damageSlider;
    public Button recallToChargingButton;
    public Button recallToRepairButton;
    public TextMeshProUGUI statusText; 
    public TextMeshProUGUI messageText; 
    public Button fixItButton; 
    private RobotStatus robotStatus;
    private RobotRecall robotRecall;
    private Coroutine messageCoroutine;

    void Start()
    {
        
        robotStatus = FindObjectOfType<RobotStatus>();
        if (robotStatus == null)
        {
            Debug.LogError("RobotStatus script not found!");
        }

        robotRecall = FindObjectOfType<RobotRecall>();
        if (robotRecall == null)
        {
            Debug.LogError("RobotRecall script not found!");
        }

       
        if (recallToChargingButton != null)
        {
            recallToChargingButton.onClick.AddListener(OnRecallToChargingButtonClicked);
        }
        else
        {
            Debug.LogError("RecallToChargingButton is not assigned!");
        }

        if (recallToRepairButton != null)
        {
            recallToRepairButton.onClick.AddListener(OnRecallToRepairButtonClicked);
        }
        else
        {
            Debug.LogError("RecallToRepairButton is not assigned!");
        }

        
        if (messageText == null)
        {
            Debug.LogError("MessageText is not assigned!");
        }
        else
        {
            messageText.text = "";
            messageText.enabled = false;
        }
       
        if (fixItButton == null)
        {
            Debug.LogError("FixItButton is not assigned!");
        }
        else
        {
           
            fixItButton.gameObject.SetActive(false);
           
            fixItButton.onClick.AddListener(OnFixItButtonClicked);
        }
    }

    void Update()
    {
        if (robotStatus != null)
        {
            
            batterySlider.value = robotStatus.batteryLevel;
            cleanlinessSlider.value = robotStatus.cleanliness;
            damageSlider.value = robotStatus.damageLevel;

           
            UpdateStatusText();
        }
    }

   
    public void ShowMessage(string message, float duration)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
        messageCoroutine = StartCoroutine(ShowMessageCoroutine(message, duration));
    }

    private IEnumerator ShowMessageCoroutine(string message, float duration)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.enabled = true;

            yield return new WaitForSeconds(duration);

            messageText.text = "";
            messageText.enabled = false;
        }
    }

    void UpdateStatusText()
    {
        if (statusText != null)
        {
            string status = $"Battery: {robotStatus.batteryLevel:F0}/200  |  Cleanliness: {robotStatus.cleanliness:F0}/100  |  Damage: {robotStatus.damageLevel:F0}/100";
            statusText.text = status;
        }
    }

    void OnRecallToChargingButtonClicked()
    {
        if (robotRecall != null)
        {
            robotRecall.Recall(robotRecall.chargingStationPoint);
            ShowMessage("Robot is returning to the charging station", 2f);
        }
    }

    void OnRecallToRepairButtonClicked()
    {
        if (robotRecall != null)
        {
            robotRecall.Recall(robotRecall.repairStationPoint);
            ShowMessage("Robot is returning to the repair station", 2f);
        }
    }

    public void ShowFixItButton()
    {
        if (fixItButton != null)
        {
            fixItButton.gameObject.SetActive(true);
        }
    }

    
    void OnFixItButtonClicked()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
