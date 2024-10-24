using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Color color;

    public SpriteRenderer spriteRenderer;
    public GunsmithData gunsmithData;
    public AudioManager audioManager;
    private Animator animator;
    private Button button;
    private void Start()
    {
        animator = GetComponent<Animator>();       
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        animator.SetTrigger("Trigger");
        audioManager.PlayBatteryPickup();
        spriteRenderer.color = color;
        gunsmithData.color = color;
    }

}
