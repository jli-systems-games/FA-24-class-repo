using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class z_SceneChange : MonoBehaviour
{
    public void z0Menu()
    {
        SceneManager.LoadScene("0Menu");
    }

    public void z1ChooseRock()
    {
        SceneManager.LoadScene("1ChooseRock");
    }

    public void z2Decorate()
    {
        SceneManager.LoadScene("2Decorate");
    }

    public void z3FoodWater()
    {
        SceneManager.LoadScene("3FoodWater");
    }

    public void z4Reveal()
    {
        SceneManager.LoadScene("4Reveal");
    }
}
