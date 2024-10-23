using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public Transform gunHoldPosition; // 玩家手持枪的位置

    void Start()
    {
        // 检查是否有选中的枪
        if (GunManager.selectedGun != null)
        {
            // 生成玩家选中的枪
            GameObject gun = Instantiate(GunManager.selectedGun, gunHoldPosition.position, gunHoldPosition.rotation);
            gun.transform.SetParent(gunHoldPosition); // 将枪附加到玩家手上

            // 确保枪的相对位置和旋转保持正确
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.Log("No gun selected from previous scene.");
        }
    }
}
