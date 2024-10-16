using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public ButtonMode buttonMode = ButtonMode.Start;

    public Canvas gameCanvas,infoCanvas;

    private Button button;

   public enum ButtonMode
    {
        Start, Close
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        Time.timeScale = 0;
    }
    void OnButtonClick()
    {
       switch (buttonMode) 
        {
            case ButtonMode.Start:
                gameCanvas.enabled = false; infoCanvas.enabled = true;
                Time.timeScale = 0;
                break;
            case ButtonMode.Close:
                gameCanvas.enabled = true; infoCanvas.enabled = false;
                Time.timeScale = 1;
                break;       
        }
    }
}
