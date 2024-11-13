using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitStats")]
public class UnitStats : ScriptableObject
{
    public string unitName;
    public int health;
    public int attackPower;
    public float attackSpeed;
    public int team;
}

