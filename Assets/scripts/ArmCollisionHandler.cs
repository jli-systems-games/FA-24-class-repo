using UnityEngine;

public class ArmCollisionHandler : MonoBehaviour
{
    public ArmWrestlingGame gameManager; // ���õ� ArmWrestlingGame ������

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedPad") && this.CompareTag("RedArm"))
        {
            gameManager.PlayerRedWins(); // ���ú�ɫ��һ�ʤ�ķ���
        }
        else if (other.CompareTag("BluePad") && this.CompareTag("BlueArm"))
        {
            gameManager.PlayerBlueWins(); // ������ɫ��һ�ʤ�ķ���
        }
    }
}
