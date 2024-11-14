using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTag : MonoBehaviour
{
    public UnitType unitType;
    public enum UnitType
    {
        Infantry,
        Vehicle,
    }
}
