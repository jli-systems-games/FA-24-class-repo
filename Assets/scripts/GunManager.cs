using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GameObject selectedGun; // 保存玩家当前持有的枪

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // 保持GunManager在场景切换时不销毁
    }

    // 保存玩家当前持有的枪
    public static void SetSelectedGun(GameObject gun)
    {
        selectedGun = gun;
    }

    // 清除枪数据
    public static void ClearSelectedGun()
    {
        selectedGun = null;
    }
}
