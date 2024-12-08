using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Z_BreakingSwitch()
    {
        SceneManager.LoadScene("BreakDancign");
    }

    public void Z_HorseSwitch()
    {
        SceneManager.LoadScene("Equestrian");
    }

    public void Z_PoleSwitch()
    {
        SceneManager.LoadScene("PoleVault");
    }

    public void Z_RunningSwitch()
    {
        SceneManager.LoadScene("Running");
    }

    public void Z_TrampolineSwitch()
    {
        SceneManager.LoadScene("Trampoline");
    }

    public void Z_WeightSwitch()
    {
        SceneManager.LoadScene("Weight");
    }
}
