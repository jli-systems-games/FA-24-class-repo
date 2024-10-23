using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public SwitchWeapon sw;
   

    public Recoil [] recoil;
    bool canShoot;
    public static bool canPropulse;
   public float propForce;
    Rigidbody rb;
    public bool asAshoot;
    public bool asReload;
    public Camera cam;

    public float range;
    public GameObject particleHit;
    public GameObject muzzle;

    public int [] munition;
    public int [] maxMunition;
    public static int indexAmmo;
    public static bool isArm;
    void Start()
    {
        canShoot = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       if(!isArm)
        {
            if (Input.GetMouseButton(0) && canShoot && munition[indexAmmo] != 0)
            {
                recoil[0].Recoilfire();
                recoil[1].Recoilfire();

                munition[indexAmmo] -= 1;
                asAshoot = true;
                Invoke("Shoot", 0.1f);
                canShoot = false;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                reload();
            }
        }
        

        
    }

    void reload()
    {
        munition[indexAmmo] = maxMunition[indexAmmo];
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Instantiate(particleHit, hit.point, Quaternion.identity);

            if (canPropulse)
            {
                rb.AddForce(transform.up * propForce, ForceMode.Impulse);
            }
        }

        Instantiate(muzzle, gunTip.position, gunTip.transform.rotation);
        
        canShoot=true;
    }

    public static Transform  gunTip;
}
