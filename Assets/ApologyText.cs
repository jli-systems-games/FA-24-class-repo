using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ApologyText : MonoBehaviour
{
    public TextMeshProUGUI apologyText;

    public Image colleenImage;
    public Sprite colleenClosed;
    public Sprite colleenOpen;

    public string[] textMessages;
    private int currentMessageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeImage(colleenOpen);
            ChangeText();
        }

        // Revert the sprite when the space bar is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeImage(colleenClosed);
        }
    }

    void ChangeImage(Sprite sprite)
    {
        colleenImage.sprite = sprite;
    }

    void ChangeText()
    {
        if (textMessages.Length > 0)
        {
            apologyText.text = textMessages[currentMessageIndex];
            currentMessageIndex = (currentMessageIndex + 1) % textMessages.Length;
        }
    }
}
