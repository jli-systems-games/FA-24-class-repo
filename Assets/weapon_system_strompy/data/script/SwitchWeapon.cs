using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchWeapon : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Transform [] weapon;

    public static bool canPass;
    public int maxWeapon;
    void Start()
    {
        SelectWeapon();
        weapon = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            weapon[i] = transform.GetChild(i);

    }

    // Update is called once per frame
    void Update()
    {
        int previous = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0 || canPass)
        {
            if(selectedWeapon >= transform.childCount -1)
            {
                selectedWeapon = 0;
            }
            else
            {
                
                
                    selectedWeapon++;
                
               
              
            }
          
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
            {
               
                   selectedWeapon = transform.childCount - 1;
                
               
            }
            else
            {
               

                    selectedWeapon--;


              


            }

        }

        if(maxWeapon == selectedWeapon)
        {
            weapon[previous].GetComponent<weaponTake>().ForceDrop = true;
        }

        if(previous != selectedWeapon )
        {
            SelectWeapon();
            

          
        }

        if (canPass)
        {
           
            weapon = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
                weapon[i] = transform.GetChild(i);

            selectedWeapon = weapon.Length - 1;
            SelectWeapon();
            
            canPass = false;
        }
        
    }

    public Transform [] trans;

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if( i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }


    }

    public static bool asWeapon;
}
