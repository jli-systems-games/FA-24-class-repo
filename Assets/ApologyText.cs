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
        textMessages = new string[]
        {
            "Hi. It's been a while since you saw my face.",
            "All aboard the toxic gossip train chugging down the tracks of misinformation.",
            "Toxic gossip traaaain. ",
            "Uh hi everyone, I've been wanting to come online and talk to you about a few things um even though my team has strongly advised me to not say what I want to say I recently realized that they never said that I couldn't sing what I want to say. So...",
            "What oh you don't care oh okay I thought you wanted me to take accountability but that's not the point of your mob mentality, is it?",
            "oh I'm sorry I didn't realize that all of you are perfect, so please criticize me",
            "Now. Have I made some jokes in poor taste? yes. Have I made lots of dumb mistakes? yes.",
            "oh I just wanted to say that um the thing I've ever groomed is my two Persian cats.",
            "I'm not a groomer I'm just a loser who didn't understand I shouldn't respond to fans and I'm not a predator, even though a lot of you think so, because five years ago I made a fart joke.",
        };
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
