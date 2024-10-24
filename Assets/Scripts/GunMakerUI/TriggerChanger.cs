using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerChanger : MonoBehaviour
{   

    public ButtonType buttonType = ButtonType.None; // 默认是None

    public GunsmithData gunsmithData;
    public AudioManager audioManager;
    private Animator animator;
    private Button button;
    public Animator gunAnimator;

    // 枚举触发器类型
    public enum ButtonType
    {
        None,
        Sports,
        Upgrade,
        Illegal,
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
        gunAnimator.SetTrigger("Trigger");
        // 播放音效
        audioManager.PlaySwitchWeapon();

        // 根据按钮类型设置对应的触发器精灵和 GunsmithData 中的触发器类型
        switch (buttonType)
        {
            case ButtonType.None:
                gunsmithData.triggerType = GunsmithData.TriggerType.None;
                break;

            case ButtonType.Sports:
                gunsmithData.triggerType = GunsmithData.TriggerType.Sports;
                break;

            case ButtonType.Upgrade:
                gunsmithData.triggerType = GunsmithData.TriggerType.Upgrade;
                break;

            case ButtonType.Illegal:
                gunsmithData.triggerType = GunsmithData.TriggerType.Illegal;
                break;
        }
    }
}
