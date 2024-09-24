using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enums are "structs" which is why they aren't in the class
//this makes it so it can be referenced by other scripts in our project
public enum BossStage
{
    Stage01,
    Stage02
}
//enums don't technically "do" anything - they just put labels on a variable that is human readable
//they are great for making "states"

public class Scripts_Boss : MonoBehaviour
{
    //here we walked through what we might do to add a boss to our existing scripts

    private Scripts_GameManager _gameManager;
    private Scripts_MusicManager _musicManager;


    public BossStage currentStage = BossStage.Stage01;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<Scripts_GameManager>();
        _musicManager = FindObjectOfType<Scripts_MusicManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if current enemies >5 & not stage 1

    }

    void TakeDamage() 
    {
        //adjust the health here, then check if something needs to change
        //if  >50, 
    
    }
    
    void ChangeStage()
    {
        //change currentStage
        //ping musicmanager
    }

    void Attack()
    {
        //
    }

}
