using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_GameManager : MonoBehaviour
{
    public GameObject[] combatants;
    public int combatantCount;
    //ui buttons
    //start coordinates?
    
    
    // Start is called before the first frame update
    void Start()
    {
        combatants = new GameObject[2];
        combatantCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCombatant(GameObject combatant)
    {
        if (combatantCount < combatants.Length) 
        {
            combatants[combatantCount] = combatant;
            combatantCount++; 
        }
        
        if(combatantCount == combatants.Length)
        {
            //disable buttons
            StartCombat();//start fight
        }
    }

    public void StartCombat()
    {
        //instantiate combatants[0] in scene
        //instantiate combatnats[1] in scene
    }
}
