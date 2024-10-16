using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
           UnitHealth unitHealth = collision.GetComponent<UnitHealth>();
            unitHealth.GetFeed();
            Destroy(gameObject);
        }

        if (collision.CompareTag("DeadLine"))
        {           
            Destroy(gameObject);
        }
    }
}
