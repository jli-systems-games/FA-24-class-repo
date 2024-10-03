using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeshadow : MonoBehaviour
{
    AudioSource audioSource;

    public GameObject eyeshadowCanvas;
    public GameObject regularCanvas;
    public GameObject thiss;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public checkGB checkStatus;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying && checkStatus.ended == false)
        {
            regularCanvas.gameObject.SetActive(false);
            eyeshadowCanvas.gameObject.SetActive(true);
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            thiss.gameObject.SetActive(false);
        }
    }
}
