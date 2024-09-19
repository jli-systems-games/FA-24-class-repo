using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameDeadArea : MonoBehaviour
{
    public MiniGameLevelController controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller.GameFail();
        }
    }
}
