using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour
{
    public Sprite
        tGrip,         // T型握把
        thumbGrip,     // 拇指握把
        angledGrip,    // 角度握把
        lightGrip,     // 轻型握把
        grenadeGrip,   // 手榴弹握把
        none;          // 无握把

    public PlayerData playerData;  // 引用 PlayerData 以获取 GunsmithData
    private SpriteRenderer spriteRenderer;
    private GunsmithData gunsmithData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunsmithData = playerData.gunsmithData;

        // 根据握把类型切换不同的握把 Sprite
        switch (gunsmithData.gripType)
        {
            case GunsmithData.GripType.TGrip:
                spriteRenderer.sprite = tGrip;
                break;
            case GunsmithData.GripType.ThumbGrip:
                spriteRenderer.sprite = thumbGrip;
                break;
            case GunsmithData.GripType.AngledGrip:
                spriteRenderer.sprite = angledGrip;
                break;
            case GunsmithData.GripType.LightGrip:
                spriteRenderer.sprite = lightGrip;
                break;
            case GunsmithData.GripType.Grenade:
                spriteRenderer.sprite = grenadeGrip;
                break;
            case GunsmithData.GripType.None:
                spriteRenderer.sprite = none;
                break;
        }
    }
}
