using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public Transform player;  // 玩家的位置，记得在Unity Inspector中指定玩家

    void OnMouseDown()
    {
        
        float distance = Vector3.Distance(player.position, transform.position);

        
        if (distance <= 3f)
        {
            
            Instantiate(destroyedVersion, transform.position, transform.rotation);

            
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("距离过远，无法破坏物体");
        }
    }
}
