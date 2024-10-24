using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Sprite
        shortSBarrel,  // 短枪管 (ShortS)
        shortMBarrel,  // 中短枪管 (ShortM)
        shortLBarrel,  // 长短枪管 (ShortL)
        longSBarrel,   // 短长枪管 (LongS)
        longMBarrel,   // 中长枪管 (LongM)
        longLBarrel;   // 长枪管 (LongL)

    public PlayerData playerData;  // 引用 PlayerData 以获取 GunsmithData
    private SpriteRenderer spriteRenderer;
    private GunsmithData gunsmithData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunsmithData = playerData.gunsmithData;

        // 根据枪管类型切换不同的枪管 Sprite
        switch (gunsmithData.barrelType)
        {
            case GunsmithData.BarrelType.ShortS:
                spriteRenderer.sprite = shortSBarrel;
                break;
            case GunsmithData.BarrelType.ShortM:
                spriteRenderer.sprite = shortMBarrel;
                break;
            case GunsmithData.BarrelType.ShortL:
                spriteRenderer.sprite = shortLBarrel;
                break;
            case GunsmithData.BarrelType.LongS:
                spriteRenderer.sprite = longSBarrel;
                break;
            case GunsmithData.BarrelType.LongM:
                spriteRenderer.sprite = longMBarrel;
                break;
            case GunsmithData.BarrelType.LongL:
                spriteRenderer.sprite = longLBarrel;
                break;
        }
    }
}
