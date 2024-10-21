using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    // maybe not the best practice here?
    // we could also make the wheelSelectMenu's event static
    // so we don't have to worry about a public reference here 
    public WheelSelectMenu wheelSelectMenu; 

    // a more complete version of this might have a child script
    // for the chassis that would update the wheels accordingly
    public Transform FrontLeftWheelTransform;
    public Transform FrontRightWheelTransform;
    public Transform BackLeftWheelTransform;
    public Transform BackRightWheelTransform;

    private void Awake()
    {
        wheelSelectMenu.UpdateWheelType += ChangeWheel;
    }

    private void OnDestroy()
    {
        wheelSelectMenu.UpdateWheelType -= ChangeWheel;
    }
    
    void ChangeWheel(WheelStats stats)
    {
        //if we already have wheels instantiated, get rid of them!
        if (FrontLeftWheelTransform.childCount > 0)
        {
            Destroy(FrontLeftWheelTransform.GetChild(0).gameObject);
            Destroy(FrontRightWheelTransform.GetChild(0).gameObject);
            Destroy(BackLeftWheelTransform.GetChild(0).gameObject);
            Destroy(BackRightWheelTransform.GetChild(0).gameObject);
        }

        //add new wheels
        Instantiate(stats.WheelPrefab, FrontLeftWheelTransform);
        Instantiate(stats.WheelPrefab, FrontRightWheelTransform);
        Instantiate(stats.WheelPrefab, BackLeftWheelTransform);
        Instantiate(stats.WheelPrefab, BackRightWheelTransform);
    }
}
