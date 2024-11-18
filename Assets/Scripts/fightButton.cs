using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fightButton : MonoBehaviour
{
    public fighterVariable fighterScript;
    public wizardVariable wizardScript;


    // Start is called before the first frame update
    void Start()
    {
        //fighterScript = GameObject.Find("fiughteer").GetComponent<fighterVariable>();
        //wizardScript = GameObject.Find("wizard").GetComponent<wizardVariable>();


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnFightPress()
    {
        //dodgeChance = Random.Range(0, 3);
        ////call a random range if the random range > 2 then dodge
        ///
        for (int i = 0; i < 10; i++)
        {
            fighterScript.health = fighterScript.health - wizardScript.attack;
            wizardScript.health = wizardScript.health - fighterScript.attack;


            //WaitForSeconds(4);

            if (fighterScript.health < 0 )
            {
                //wizard won
            }
            else if (wizardScript.health < 0)
            {
                //fighter won
            }

        }

 
    }
}
