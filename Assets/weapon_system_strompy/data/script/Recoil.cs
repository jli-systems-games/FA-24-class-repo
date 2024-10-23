using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Vector3 currentRotation; 
    private Vector3 targetRotation;
    //Hipfire Recoil
    [SerializeField] private float recoilX; 
    [SerializeField] private float recoily;
    [SerializeField] private float recoilz;
    //Settings
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;
    
    void Start()
    {

    }

     void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime); 
        transform.localRotation = Quaternion.Euler(currentRotation);
       
    }

     public void Recoilfire()
    {
        Debug.Log("cc");
        targetRotation += new Vector3(recoilX, Random.Range(-recoily, recoily), Random.Range(-recoilz, recoilz));
    }

}
