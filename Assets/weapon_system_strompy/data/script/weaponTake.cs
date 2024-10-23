using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponTake : MonoBehaviour
{
    public float range;
    public Transform parent;
    public Transform endPosition;

    public GameObject Player;
    Rigidbody rb;
    BoxCollider box;

    public bool asTake;
    public bool ForceDrop;
    public Transform[] child2;
    int index;

    public bool isArm;
    public bool propulsionGun;
    void Start()
    {
        if (!isArm)
        {
            rb = GetComponent<Rigidbody>();
            box = GetComponent<BoxCollider>();

            for (int i = 0; i < child2.Length; i++)
            {
                index = i;


            }
            child2 = transform.GetComponentsInChildren<Transform>();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isArm)
        {
            float distance = Vector3.Distance(Player.transform.position, transform.position);
            rb.isKinematic = asTake;
            if (distance <= range && !asTake)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    take();
                    stop = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab) || ForceDrop)
            {
                drop();
                asTake = false;
                stop = false;
                ForceDrop = false;
            }

            if (asTake && !stop)
            {
                transform.position = Vector3.Lerp(transform.position, endPosition.position, 8 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, endPosition.rotation, 8 * Time.deltaTime);
                Invoke("stopAll", 2);
            }

            if (asTake)
            {
                PlayerShoot.canPropulse = propulsionGun;
                PlayerShoot.indexAmmo = weaponNbr;
                PlayerShoot.gunTip = gunTip.transform;
            }
        }
       

        PlayerShoot.isArm = isArm;

    }

    public Transform gunTip;
    public int weaponNbr;

    bool stop;
    void stopAll()
    {
        
        stop = true;
    }
    void drop()
    {
       
        box.enabled = true;
        transform.parent = null;

        int LayerIgnoreRaycast = LayerMask.NameToLayer("Interactive");
        gameObject.layer = LayerIgnoreRaycast;



        for (int i = 0; i < child2.Length; i++)
        {
            child2[i].gameObject.layer = LayerIgnoreRaycast;



        }
    }

    void take()
    {

        SwitchWeapon.canPass = true;
        box.enabled = false;
        transform.parent = parent.transform;

        int LayerIgnoreRaycast = LayerMask.NameToLayer("weapon");
        gameObject.layer = LayerIgnoreRaycast;

        for (int i = 0; i < child2.Length; i++)
        {
            child2[i].gameObject.layer = LayerIgnoreRaycast;



        }
        asTake = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;    
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
