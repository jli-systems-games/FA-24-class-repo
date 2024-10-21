using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "accessory")]
public class characterObject : ScriptableObject
{
    public GameObject[] headAcessories;
    public Vector3 hatPlacement;

    public GameObject[] faceAccessories;
    public Vector3[] facePlacement;


}
