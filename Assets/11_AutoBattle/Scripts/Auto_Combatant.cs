using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combatant", menuName = "ScriptableObjects/Combatant", order = 2)]
public class Auto_Combatant : ScriptableObject
{
    public string combatantName = string.Empty;

    public int combatantHealth, combatantStrength, combatantSpeed;
}
