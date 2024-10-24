using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickables : MonoBehaviour
{
    public Type type;
    public enum Type
    { 
        Bullet,
        Medic,
        Battery,
        Water,
    }
    private AudioManager audioManager;
    private GameManager gameManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type) 
            { 
                case Type.Bullet:
                    gameManager.bulletCount += 40;
                    gameManager.UpdateBulletUI();
                    audioManager.PlayBulletPickup();
                    break; 
                case Type.Medic:
                    gameManager.AddHealth();
                    audioManager.PlayMedicPickup();
                    break; 
                case Type.Battery:
                    gameManager.AddPower();
                    audioManager.PlayBatteryPickup();
                    break;
                case Type.Water:
                    gameManager.AddSanity();
                    audioManager.PlayWaterPickup();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
