using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponShoot : MonoBehaviour
{
    public PlayerShoot playerShoot;
    public GameObject[] bullet;
    //public GameObject[] muzzle;
    //public GameObject[] smoke;
    // public Gameobject[] sfx

    // same process as the bullet

    int index;
    public SwitchWeapon sw;

    public int[] munition;
    public int[] maxMunition;

    public Transform[] gunTip; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        index = sw.selectedWeapon -1;

        if (playerShoot.asAshoot)
        {
            Instantiate(bullet[index], gunTip[index].position, gunTip[index].rotation);
            munition[index] -= 1;
            playerShoot.asAshoot = false;
        }

        if(playerShoot.asReload)
        {
            munition[index] = maxMunition[index];
            playerShoot.asReload = false;
        }
    }
}
