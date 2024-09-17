using UnityEngine;

public class ArmCollisionHandler : MonoBehaviour
{
    public ArmWrestlingGame gameManager; // 引用到 ArmWrestlingGame 管理器

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedPad") && this.CompareTag("RedArm"))
        {
            gameManager.PlayerRedWins(); // 调用红色玩家获胜的方法
        }
        else if (other.CompareTag("BluePad") && this.CompareTag("BlueArm"))
        {
            gameManager.PlayerBlueWins(); // 调用蓝色玩家获胜的方法
        }
    }
}
