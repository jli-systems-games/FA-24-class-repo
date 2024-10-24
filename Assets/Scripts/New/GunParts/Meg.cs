using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meg : MonoBehaviour
{
    public Sprite
        veryLightMeg,   // 超轻弹匣
        lightMeg,       // 轻型弹匣
        standardMeg,    // 标准弹匣
        standardExMeg,  // 扩展标准弹匣
        drumMeg,        // 鼓式弹匣
        doubleDrumMeg;  // 双鼓弹匣

    public PlayerData playerData;  // 引用 PlayerData 以获取 GunsmithData
    private SpriteRenderer spriteRenderer;
    private GunsmithData gunsmithData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunsmithData = playerData.gunsmithData;

        // 根据弹匣类型切换不同的弹匣 Sprite
        switch (gunsmithData.megType)
        {
            case GunsmithData.MegType.VeryLight:
                spriteRenderer.sprite = veryLightMeg;
                break;
            case GunsmithData.MegType.Light:
                spriteRenderer.sprite = lightMeg;
                break;
            case GunsmithData.MegType.Standard:
                spriteRenderer.sprite = standardMeg;
                break;
            case GunsmithData.MegType.StandardEx:
                spriteRenderer.sprite = standardExMeg;
                break;
            case GunsmithData.MegType.Drum:
                spriteRenderer.sprite = drumMeg;
                break;
            case GunsmithData.MegType.DoubleDrum:
                spriteRenderer.sprite = doubleDrumMeg;
                break;
        }
    }
}
