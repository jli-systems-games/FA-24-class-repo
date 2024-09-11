using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonChallenge : MonoBehaviour
{
    public Button challengeButton;
    public TextMeshProUGUI victory;
    public TextMeshProUGUI failed;
    private float timer = 0f;
    private bool buttonClicked = false; // checks if button is clicked
    public float timeLimit = 3f;

    void Start()
    {
        challengeButton.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (!buttonClicked)
        {
            timer += Time.deltaTime;
            if (timer > timeLimit)
            {
                // failed
                failed.gameObject.SetActive(true);
                challengeButton.interactable = false;
            }
        }
    }

    void OnButtonClick()
    {
        if (timer <= timeLimit)
        {
            // victory
            buttonClicked = true;
            victory.gameObject.SetActive(true);
            challengeButton.interactable = false;
        }
    }
}
