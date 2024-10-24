using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    public Sprite
        standardStock,
        heavyStock,
        lightStock,
        sniperStock,
        none;

    public PlayerData playerData;
    private SpriteRenderer spriteRenderer;
    private GunsmithData gunsmithData;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunsmithData = playerData.gunsmithData;

        switch (gunsmithData.stockType)
        {
            case GunsmithData.StockType.Standard:
                spriteRenderer.sprite = standardStock;
                break;
            case GunsmithData.StockType.Heavy:
                spriteRenderer.sprite = heavyStock;
                break;
            case GunsmithData.StockType.Light:
                spriteRenderer.sprite = lightStock;
                break;
            case GunsmithData.StockType.Sniper:
                spriteRenderer.sprite = sniperStock;
                break;
            case GunsmithData.StockType.None:
                spriteRenderer.sprite = none;
                break;
        }
    }

}
