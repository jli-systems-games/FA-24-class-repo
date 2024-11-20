using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Weapon
}

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/NItem", order = 3)]
public class Item:ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    //public float itemWeight;
}
