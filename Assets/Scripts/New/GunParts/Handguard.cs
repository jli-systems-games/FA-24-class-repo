using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handguard : MonoBehaviour
{
    public Sprite
        longHandguard,  // 长护木
        shortHandguard, // 短护木
        none;           // 无护木

    public PlayerData playerData;  // 引用 PlayerData 以获取 GunsmithData
    private SpriteRenderer spriteRenderer;
    private GunsmithData gunsmithData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunsmithData = playerData.gunsmithData;

        // 根据护木类型切换不同的护木 Sprite
        switch (gunsmithData.handguardType)
        {
            case GunsmithData.HandguardType.Long:
                spriteRenderer.sprite = longHandguard;
                break;
            case GunsmithData.HandguardType.Short:
                spriteRenderer.sprite = shortHandguard;
                break;
            case GunsmithData.HandguardType.None:
                spriteRenderer.sprite = none;
                break;
        }
    }
}
