using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    idle,
    regularBowels,
    diarrhea,
    dead
}
public class Pigeon : ScriptableObject
{
    public GameObject pigeon;

    public int HP;


}
