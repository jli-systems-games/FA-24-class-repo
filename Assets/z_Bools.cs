using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class z_Bools : MonoBehaviour
{
    //rock type
    static public bool SedRock;
    static public bool IgnRock;
    static public bool MetRock;

    //food
    static public bool SedFood;
    static public bool IgnFood;
    static public bool MetFood;

    // water
    static public bool IgnWater; //less
    static public bool MetWater; //med
    static public bool SedWater; //more


    void Start()
    {
        DontDestroyOnLoad(gameObject);

        SedRock = false;
        IgnRock = false;
        MetRock = false;

        SedFood = false;
        IgnFood = false;
        MetFood = false;

        IgnWater = false;
        MetWater = false;
        SedWater = false;
    }

    public void zSedRock()
    {
        SedRock = true;
        IgnRock = false;
        MetRock = false;
    }

    public void zIgnRock()
    {
        SedRock = false;
        IgnRock = true;
        MetRock = false;
    }

    public void zMetRock()
    {
        SedRock = false;
        IgnRock = false;
        MetRock = true;
    }

    public void zSedFood()
    {
        SedFood = true;
        IgnFood = false;
        MetFood = false;
    }

    public void zIgnFood()
    {
        SedFood = false;
        IgnFood = true;
        MetFood = false;
    }

    public void zMetFood()
    {
        SedFood = false;
        IgnFood = false;
        MetFood = true;
    }

    public void zIgnWater()
    {
        IgnWater = true;
        MetWater = false;
        SedWater = false;
    }

    public void zMetWater()
    {
        IgnWater = false;
        MetWater = true;
        SedWater = false;
    }

    public void zSedWater()
    {
        IgnWater = false;
        MetWater = false;
        SedWater = true;
    }
}
