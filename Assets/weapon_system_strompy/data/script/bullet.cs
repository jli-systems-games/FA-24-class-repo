using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public bool isPropulsionBullet;
    public float speed;
   Rigidbody rb;
    void Start()
    {
        StartCoroutine(autoDestruction());
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.right * speed, ForceMode.Impulse);
        if (isPropulsionBullet)
        {
            StartCoroutine(cant());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPropulsionBullet)
        {
            PlayerShoot.canPropulse = true;
            
        }

        Destroy(gameObject);
    }
    IEnumerator autoDestruction()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }

    IEnumerator cant()
    {
        yield return new WaitForSeconds(0.3f);
        isPropulsionBullet = false;
    }
}
