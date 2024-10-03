using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI startText1;
    public TextMeshProUGUI startText2;

    public Image colleenImage;
    public Sprite newSprite;
    public Image keyboardImage;

    public AudioSource audioSource;
    public AudioClip ukeleleMusic;

    // Start is called before the first frame update
    void Start()
    {
        keyboardImage.canvasRenderer.SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            DisableText();
            ChangeSprite();
            PlayMusic();

            keyboardImage.canvasRenderer.SetAlpha(1);
        }
    }

    void DisableText()
    {
        startText1.enabled = false;
        startText2.text = "press SPACE to apologize";
    }

    void ChangeSprite()
    {
        colleenImage.sprite = newSprite;
    }

    void PlayMusic()
    {
        if (audioSource != null && ukeleleMusic != null)
        {
            audioSource.clip = ukeleleMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("no music");
        }
    }
}
