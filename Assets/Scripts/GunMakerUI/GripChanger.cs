using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GripChanger : MonoBehaviour
{
    // 声明不同握把类型的精灵
    public Sprite noneGrip, tGrip, thumbGrip, angledGrip, lightGrip, grenadeGrip;

    public ButtonType buttonType = ButtonType.None; // 默认是None

    public SpriteRenderer gunSprite;
    public GunsmithData gunsmithData;
    public AudioManager audioManager;
    private Animator animator;
    private Button button;

    // 枚举握把类型
    public enum ButtonType
    {
        None,
        TGrip,
        ThumbGrip,
        AngledGrip,
        LightGrip,
        Grenade,
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
            case ButtonType.None:
                gunSprite.sprite = noneGrip;
                gunsmithData.gripType = GunsmithData.GripType.None;
                break;

            case ButtonType.TGrip:
                gunSprite.sprite = tGrip;
                gunsmithData.gripType = GunsmithData.GripType.TGrip;
                break;

            case ButtonType.ThumbGrip:
                gunSprite.sprite = thumbGrip;
                gunsmithData.gripType = GunsmithData.GripType.ThumbGrip;
                break;

            case ButtonType.AngledGrip:
                gunSprite.sprite = angledGrip;
                gunsmithData.gripType = GunsmithData.GripType.AngledGrip;
                break;

            case ButtonType.LightGrip:
                gunSprite.sprite = lightGrip;
                gunsmithData.gripType = GunsmithData.GripType.LightGrip;
                break;

            case ButtonType.Grenade:
                gunSprite.sprite = grenadeGrip;
                gunsmithData.gripType = GunsmithData.GripType.Grenade;
                break;
        }
    }
}
