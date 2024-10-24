using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using static GunsmithData;

public class ScopeChanger : MonoBehaviour
{
    public Sprite IronSight, Holo, Mid, Long;


    public ButtonType buttonType = ButtonType.IronSight;

    public SpriteRenderer gunSprite;
    public enum ButtonType
    {
        IronSight,
        Holo,
        Mid,
        Long,
    }
    public GunsmithData gunsmithData;
    public AudioManager audioManager;

    private Button button;
    private Animator animator;
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
            case ButtonType.IronSight:
                gunSprite.sprite = IronSight;
                gunsmithData.scopeType = GunsmithData.ScopeType.IronSights;
                break;

            case ButtonType.Holo:
                gunSprite.sprite = Holo;
                gunsmithData.scopeType = GunsmithData.ScopeType.Holographic;
                break;

            case ButtonType.Mid:
                gunSprite.sprite = Mid;
                gunsmithData.scopeType = GunsmithData.ScopeType.MidRange;
                break;

            case ButtonType.Long:
                gunSprite.sprite = Long;
                gunsmithData.scopeType = GunsmithData.ScopeType.LongRange;
                break;
        }
    }
}
