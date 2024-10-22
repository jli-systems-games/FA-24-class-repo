using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "accessory")]
public class characterObject : ScriptableObject
{
    [Header("Hats")]
    public GameObject[] hats;
    public MeshRenderer[] hatsR;
    public Mesh[] hatMesh;

    [Header("Face")]
    public GameObject[] faceAccessories;
    public Mesh[] facePlacement;
    public MeshRenderer[] faceR;


}
