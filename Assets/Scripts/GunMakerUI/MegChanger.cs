using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MegChanger : MonoBehaviour
{
    public Sprite veryLightMeg, lightMeg, standardMeg, standardExMeg, drumMeg, doubleDrumMeg;

    public ButtonType buttonType = ButtonType.Standard; // 默认是Standard

    public SpriteRenderer gunSprite;
    public GunsmithData gunsmithData;
    public AudioManager audioManager;
    private Animator animator;
    private Button button;

    public enum ButtonType
    {
        VeryLight,
        Light,
        Standard,
        StandardEx,
        Drum,
        DoubleDrum,
    }

    void Start()
    {
        button = GetComponent<Button>();
        animator = GetComponent<Animator>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        animator.SetTrigger("Trigger");

        audioManager.PlaySwitchWeapon();

        switch (buttonType)
        {
            case ButtonType.VeryLight:
                gunSprite.sprite = veryLightMeg;
                gunsmithData.megType = GunsmithData.MegType.VeryLight;
                break;

            case ButtonType.Light:
                gunSprite.sprite = lightMeg;
                gunsmithData.megType = GunsmithData.MegType.Light;
                break;

            case ButtonType.Standard:
                gunSprite.sprite = standardMeg;
                gunsmithData.megType = GunsmithData.MegType.Standard;
                break;

            case ButtonType.StandardEx:
                gunSprite.sprite = standardExMeg;
                gunsmithData.megType = GunsmithData.MegType.StandardEx;
                break;

            case ButtonType.Drum:
                gunSprite.sprite = drumMeg;
                gunsmithData.megType = GunsmithData.MegType.Drum;
                break;

            case ButtonType.DoubleDrum:
                gunSprite.sprite = doubleDrumMeg;
                gunsmithData.megType = GunsmithData.MegType.DoubleDrum;
                break;       
        }
    }
}
