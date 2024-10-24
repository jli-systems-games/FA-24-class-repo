using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoup : MonoBehaviour
{
    public Sprite
        ironSights,      // 铁瞄
        holographic,     // 全息瞄准镜
        midRange,        // 中距离瞄准镜
        longRange;       // 远距离瞄准镜

    public PlayerData playerData;  // 引用 PlayerData 以获取 GunsmithData
    private SpriteRenderer spriteRenderer;
    private GunsmithData gunsmithData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunsmithData = playerData.gunsmithData;

        // 根据瞄准镜类型切换不同的瞄准镜 Sprite
        switch (gunsmithData.scopeType)
        {
            case GunsmithData.ScopeType.IronSights:
                spriteRenderer.sprite = ironSights;
                break;
            case GunsmithData.ScopeType.Holographic:
                spriteRenderer.sprite = holographic;
                break;
            case GunsmithData.ScopeType.MidRange:
                spriteRenderer.sprite = midRange;
                break;
            case GunsmithData.ScopeType.LongRange:
                spriteRenderer.sprite = longRange;
                break;
        }
    }
}
