using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public Vector3 respawnPosition;
    public float fallHeight = -2f;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (respawnPosition == Vector3.zero)
        {
            respawnPosition = new Vector3(0, 2, 0);
        }
    }

    void Update()
    {
        if (player.position.y < fallHeight)
        {
            RespawnPlayer();
        }
    }

    // Respawn the player to the respawn position
    void RespawnPlayer()
    {
        player.position = respawnPosition;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        other.transform.position = respawnPosition;

    //        Rigidbody rb = other.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.velocity = Vector3.zero;
    //        }

    //    }
    //}
}
