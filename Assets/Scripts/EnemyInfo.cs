using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Stats")]
public class EnemyInfo : ScriptableObject
{
    public string Name;
    public int Health = 0;
    public int damage = 0;

  
}
