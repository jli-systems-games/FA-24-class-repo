using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelChanger : MonoBehaviour
{
    // 声明不同枪管类型的精灵
    public Sprite shortSBarrel, shortMBarrel, shortLBarrel, longSBarrel, longMBarrel, longLBarrel;
    public Transform transform1, transform2, transform3, transform4, transform5, transform6;
    public GameObject muzzle;
    public Sprite shortHand, longHand;
    public ButtonType buttonType = ButtonType.ShortM; // 默认是ShortM

    public SpriteRenderer gunSprite,handSprite;
    public GunsmithData gunsmithData;
    public AudioManager audioManager;
    private Animator animator;
    private Button button;

    // 枚举枪管类型
    public enum ButtonType
    {
        ShortS,
        ShortM,
        ShortL,
        LongS,
        LongM,
        LongL,
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

        switch (buttonType)
        {
            case ButtonType.ShortS:
                gunSprite.sprite = shortSBarrel;                
                gunsmithData.barrelType = GunsmithData.BarrelType.ShortS;
                muzzle.transform.localPosition = transform1.localPosition;
                handSprite.sprite = shortHand;
                gunsmithData.handguardType = GunsmithData.HandguardType.Short;
                break;

            case ButtonType.ShortM:
                gunSprite.sprite = shortMBarrel;
                gunsmithData.barrelType = GunsmithData.BarrelType.ShortM;
                muzzle.transform.localPosition = transform2.localPosition;
                handSprite.sprite = shortHand;
                gunsmithData.handguardType = GunsmithData.HandguardType.Short;
                break;

            case ButtonType.ShortL:
                gunSprite.sprite = shortLBarrel;
                gunsmithData.barrelType = GunsmithData.BarrelType.ShortL;
                muzzle.transform.localPosition = transform3.localPosition;
                handSprite.sprite = shortHand;
                gunsmithData.handguardType = GunsmithData.HandguardType.Short;
                break;

            case ButtonType.LongS:
                gunSprite.sprite = longSBarrel;
                gunsmithData.barrelType = GunsmithData.BarrelType.LongS;
                muzzle.transform.localPosition = transform4.localPosition;
                handSprite.sprite = longHand;
                gunsmithData.handguardType = GunsmithData.HandguardType.Long;
                break;

            case ButtonType.LongM:
                gunSprite.sprite = longMBarrel;
                gunsmithData.barrelType = GunsmithData.BarrelType.LongM;
                muzzle.transform.localPosition = transform5.localPosition;
                handSprite.sprite = longHand;
                gunsmithData.handguardType = GunsmithData.HandguardType.Long;
                break;

            case ButtonType.LongL:
                gunSprite.sprite = longLBarrel;
                gunsmithData.barrelType = GunsmithData.BarrelType.LongL;
                muzzle.transform.localPosition = transform6.localPosition;
                handSprite.sprite = longHand;
                gunsmithData.handguardType = GunsmithData.HandguardType.Long;
                break;
        }
    }
}
