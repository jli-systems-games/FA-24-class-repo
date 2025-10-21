using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NormalState : SubmarineBaseState
{
    private float timeToSwitch = 5f;
    private float timer = 0f;

    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "";

        Debug.Log("Submarine reset to normal state.");

        Time.timeScale = 1.5f;

        ChangeSensors(submarine);
    }

    public override void UpdateState(SubmarineStateManager submarine)
    {
        // Timer to switch to another state
        timer += Time.deltaTime;
        if (timer >= timeToSwitch)
        {
            SwitchStates(submarine);
            timer = 0f;
        }
    }

    public override void ChangeSensors(SubmarineStateManager submarine)
    {
        ResetCentralControlPanel(submarine);
        ResetDrivingPanel(submarine);
        ResetBallastPanel(submarine);
        ResetCommunicationsPanel(submarine);
        ResetEmergencySystems(submarine);
        //ResetWeapons(submarine);
    }

    public override void ImplodeState(SubmarineStateManager submarine)
    {
        // This state has no specific implode behavior
    }

    private void ResetCentralControlPanel(SubmarineStateManager submarine)
    {
        submarine.WaterSalinity.value = 50;
        submarine.WaterTemperature.value = 50;
        submarine.PressureSensor.value = 50;
        submarine.WaterDensity.value = 50;

        foreach (Slider slider in submarine.ElectricitySwitches)
        {
            slider.value = 0;
        }
    }

    private void ResetDrivingPanel(SubmarineStateManager submarine)
    {
        submarine.DepthGauge.value = 50;
        submarine.Anchor.isOn = false;
        submarine.Sonar.isOn = false;
        submarine.PeriscopeControls.SetActive(false);
        submarine.SteeringWheel.SetActive(false);
        submarine.PeriscopeView.SetActive(false);
        submarine.SonarArray.SetActive(false);

        foreach (Slider slider in submarine.PropulsionShaft)
        {
            slider.value = 0;
        }

        foreach (Slider slider in submarine.ThrusterList)
        {
            slider.value = 0;
        }
    }

    private void ResetBallastPanel(SubmarineStateManager submarine)
    {
        submarine.BuoyantSensor.value = 0;

        foreach (Slider slider in submarine.TrimTankSensor)
        {
            slider.value = 50;
        }

        foreach (Slider slider in submarine.BallastTankSensor)
        {
            slider.value = 50;
        }

        foreach (Toggle toggle in submarine.VentToggles)
        {
            toggle.isOn = false;
        }
    }

    private void ResetCommunicationsPanel(SubmarineStateManager submarine)
    {
        submarine.AntennaStatus.isOn = false;
        submarine.RadioDial.SetActive(false);
    }

    private void ResetEmergencySystems(SubmarineStateManager submarine)
    {
        submarine.BlowSystemPrefab.isOn = false;
    }

    private void ResetWeapons(SubmarineStateManager submarine)
    {
        foreach (Button button in submarine.TorpedoFwdButtons)
        {
            button.interactable = false;
        }

        foreach (Button button in submarine.TorpedoVLSButtons)
        {
            button.interactable = false;
        }

        foreach (Button button in submarine.LoadMissileButtons)
        {
            button.interactable = false;
        }

        foreach (Button button in submarine.LaunchMissileButtons)
        {
            button.interactable = false;
        }
    }

    private void SwitchStates(SubmarineStateManager submarine)
    {
        SubmarineBaseState nextState = PickRandomState(submarine);
        submarine.SwitchState(nextState);
    }

    private SubmarineBaseState PickRandomState(SubmarineStateManager submarine)
    {
        SubmarineBaseState[] states = {
            submarine.depthControlState,
            // Uncomment the states you want to include in the random selection
            //submarine.silentRunningState,
            //submarine.leakDetectionState,
            //submarine.navigationHazardState,
            //submarine.misileLaunchState,
            submarine.engineRestartState,
            //submarine.waterChangesState
        };

        // Pick a random state from the array
        return states[Random.Range(0, states.Length)];
    }
}
