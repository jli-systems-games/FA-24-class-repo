using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerImageCarrier : MonoBehaviour
{
    public Sprite[] skinSprites;  // 存储皮肤的 Sprite 数组，顺序与 Skin 枚举对应
    public Sprite[] faceSprites;  // 存储头像的 Sprite 数组，顺序与 Face 枚举对应

    // 获取对应皮肤的 Sprite
    public Sprite GetSkinSprite(PlayerImageManager.Skin skin)
    {
        int skinIndex = (int)skin;  // 将 Skin 枚举值转换为索引
        if (skinIndex >= 0 && skinIndex < skinSprites.Length)
        {
            return skinSprites[skinIndex];  // 返回对应索引的皮肤 Sprite
        }
        else
        {
            Debug.LogError("Invalid skin index");
            return null;
        }
    }

    // 获取对应头像的 Sprite
    public Sprite GetFaceSprite(PlayerImageManager.Face face)
    {
        int faceIndex = (int)face;  // 将 Face 枚举值转换为索引
        if (faceIndex >= 0 && faceIndex < faceSprites.Length)
        {
            return faceSprites[faceIndex];  // 返回对应索引的头像 Sprite
        }
        else
        {
            Debug.LogError("Invalid face index");
            return null;
        }
    }
}
