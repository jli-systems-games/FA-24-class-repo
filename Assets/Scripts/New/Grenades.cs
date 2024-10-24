using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    public GameObject boomEffect,damageArea;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            audioManager.PlaySwitchWeapon();
            Instantiate(damageArea, transform.position, Quaternion.identity);
            Instantiate(boomEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
       
    }
    private void Update()
    {
        if (transform.position.y < 0) { Destroy(gameObject); }
    }
}
