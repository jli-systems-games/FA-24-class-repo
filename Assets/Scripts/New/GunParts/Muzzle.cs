using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public Sprite
        silencer,      // 消音器
        compensator,   // 补偿器
        none;          // 无枪口配件

    public PlayerData playerData;  // 引用 PlayerData 以获取 GunsmithData
    private SpriteRenderer spriteRenderer;
    private GunsmithData gunsmithData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunsmithData = playerData.gunsmithData;

        // 根据枪口类型切换不同的枪口 Sprite
        switch (gunsmithData.muzzleType)
        {
            case GunsmithData.MuzzleType.Silencer:
                spriteRenderer.sprite = silencer;
                break;
            case GunsmithData.MuzzleType.Compensator:
                spriteRenderer.sprite = compensator;
                break;
            case GunsmithData.MuzzleType.None:
                spriteRenderer.sprite = none;
                break;
        }
    }
}
