using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using static GunsmithData;

public class StockChanger : MonoBehaviour
{
    public Sprite noneStock, standardStock, heavyStock, lightStock, sniperStock;


    public ButtonType buttonType = ButtonType.None;

    public SpriteRenderer gunSprite;
    public enum ButtonType
    { 
        None,
        Standard,
        Heavy,
        Light,
        Sniper,
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
            case ButtonType.None:
                gunSprite.sprite = noneStock;
                gunsmithData.stockType = GunsmithData.StockType.None;
                break;

            case ButtonType.Standard:
                gunSprite.sprite = standardStock;
                gunsmithData.stockType = GunsmithData.StockType.Standard;
                break;

            case ButtonType.Heavy:
                gunSprite.sprite = heavyStock;
                gunsmithData.stockType = GunsmithData.StockType.Heavy;
                break;

            case ButtonType.Light:
                gunSprite.sprite = lightStock;
                gunsmithData.stockType = GunsmithData.StockType.Light;
                break;

            case ButtonType.Sniper:
                gunSprite.sprite = sniperStock;
                gunsmithData.stockType = GunsmithData.StockType.Sniper;
                break;
        }
    }
}
