using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuzzleChanger : MonoBehaviour
{
    // 声明不同枪口类型的精灵
    public Sprite silencerMuzzle, compensatorMuzzle, noneMuzzle;

    public ButtonType buttonType = ButtonType.None; // 默认是None

    public SpriteRenderer gunSprite;
    public GunsmithData gunsmithData;
    public AudioManager audioManager;
    private Animator animator;
    private Button button;

    // 枚举枪口类型
    public enum ButtonType
    {
        Silencer,
        Compensator,
        None,
    }

    void Start()
    {
        button = GetComponent<Button>();
        animator = GetComponent<Animator>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // 触发动画
        animator.SetTrigger("Trigger");

        // 播放音效
        audioManager.PlaySwitchWeapon();

        // 根据按钮类型设置对应的枪口精灵和 GunsmithData 中的枪口类型
        switch (buttonType)
        {
            case ButtonType.Silencer:
                gunSprite.sprite = silencerMuzzle;
                gunsmithData.muzzleType = GunsmithData.MuzzleType.Silencer;
                break;

            case ButtonType.Compensator:
                gunSprite.sprite = compensatorMuzzle;
                gunsmithData.muzzleType = GunsmithData.MuzzleType.Compensator;
                break;

            case ButtonType.None:
                gunSprite.sprite = noneMuzzle;
                gunsmithData.muzzleType = GunsmithData.MuzzleType.None;
                break;

        }
    }
}
