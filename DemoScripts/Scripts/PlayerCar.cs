using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public WheelSelectMenu wheelSelectMenu;

    public Transform FrontLeftWheelTransform;
    public Transform FrontRightWheelTransform;
    public Transform BackLeftWheelTransform;
    public Transform BackRightWheelTransform;

    // Start is called before the first frame update
    void Awake()
    {
        wheelSelectMenu.UpdateWheelType += ChangeWheel;
    }

    void ChangeWheel(WheelStats stats)
    {
        if(FrontLeftWheelTransform.childCount > 0)
        {
            Destroy(FrontLeftWheelTransform.GetChild(0).gameObject);
            Destroy(FrontRightWheelTransform.GetChild(0).gameObject);
            Destroy(BackLeftWheelTransform.GetChild(0).gameObject);
            Destroy(BackRightWheelTransform.GetChild(0).gameObject);
        }
        Instantiate(stats.WheelPrefab, FrontLeftWheelTransform);
        Instantiate(stats.WheelPrefab, FrontRightWheelTransform);
        Instantiate(stats.WheelPrefab, BackLeftWheelTransform);
        Instantiate(stats.WheelPrefab, BackRightWheelTransform);
    }
}
