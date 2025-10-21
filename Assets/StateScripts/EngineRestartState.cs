using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineRestartState : SubmarineBaseState
{
    private bool fuelGaugeStatic = false;
    private int[] correctThrusters = null;
    private int[] correctShaft = null;

    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING 0086:<br>Unexpected engine failure.";
        Debug.Log("Engine Restart Issue");

        ToggleFuelStatic(true);

        RandomizeDepthGauge(submarine);

        InitializeSystems(submarine);
    }

    public override void UpdateState(SubmarineStateManager submarine)
    {
        ReferToDepthGauge(submarine);

        if (ThrustersAndShaftsCorrect(submarine))
        {
            Debug.Log("Engine successfully restarted!");
            submarine.SwitchState(submarine.normalState);
        }
    }

    public override void ChangeSensors(SubmarineStateManager submarine)
    {
        submarine.FuelSensor.value = 50;
    }

    public override void ImplodeState(SubmarineStateManager submarine)
    {
        Debug.Log("Warning: SPARK!");
    }

    private void ToggleFuelStatic(bool pause)
    {
        fuelGaugeStatic = pause;
        Debug.Log($"Fuel Gauge Static: {fuelGaugeStatic}");
    }

    private void InitializeSystems(SubmarineStateManager submarine)
    {
        InstantiateThrusters(submarine.ThrusterList);
        InstantiateShaft(submarine.PropulsionShaft);
    }

    private void InstantiateThrusters(List<Slider> thrusterList)
    {
        if (thrusterList == null || thrusterList.Count == 0)
        {
            Debug.LogWarning("Thruster list is empty or null.");
            return;
        }

        foreach (Slider thruster in thrusterList)
        {
            thruster.value = 0;
        }
    }

    private void InstantiateShaft(List<Slider> propulsionShaft)
    {
        if (propulsionShaft == null || propulsionShaft.Count == 0)
        {
            Debug.LogWarning("Shaft list is empty or null.");
            return;
        }

        foreach (Slider shaft in propulsionShaft)
        {
            shaft.value = 0;
        }
    }

    private void RandomizeDepthGauge(SubmarineStateManager submarine)
    {
        if (Random.value < 0.5f)
        {
            submarine.DepthGauge.value = Random.Range(0f, 40f);
        }
        else
        {
            submarine.DepthGauge.value = Random.Range(60f, 100f);
        }

        Debug.Log($"Randomized Depth Gauge to: {submarine.DepthGauge.value}");
    }

    private void ReferToDepthGauge(SubmarineStateManager submarine)
    {
        if (submarine.DepthGauge.value > 60)
        {
            correctThrusters = new int[] { 2, 4 };
            Debug.Log("Technician: Activate THRUSTERS 2 and 4.");
            correctShaft = new int[] { 1, 2 };
            Debug.Log("Technician: Activate SHAFTS 1 and 2.");
        }
        else if (submarine.DepthGauge.value < 40)
        {
            correctThrusters = new int[] { 1, 3 };
            Debug.Log("Technician: Activate THRUSTERS 1 and 3.");
            correctShaft = new int[] { 3, 4 };
            Debug.Log("Technician: Activate SHAFTS 3 and 4.");
        }
    }

    private bool ThrustersAndShaftsCorrect(SubmarineStateManager submarine)
    {
        if (submarine.ThrusterList == null || submarine.PropulsionShaft == null)
        {
            Debug.LogError("ThrusterList or PropulsionShaft is not assigned.");
            return false;
        }

        // Check thrusters
        if (correctThrusters != null)
        {
            foreach (int thrusterIndex in correctThrusters)
            {
                int adjustedIndex = thrusterIndex - 1; // Convert 1-based index to 0-based index
                if (adjustedIndex < 0 || adjustedIndex >= submarine.ThrusterList.Count || submarine.ThrusterList[adjustedIndex].value <= 0)
                {
                    return false;
                }
            }
        }

        // Check shafts
        if (correctShaft != null)
        {
            foreach (int shaftIndex in correctShaft)
            {
                int adjustedIndex = shaftIndex - 1; // Convert 1-based index to 0-based index
                if (adjustedIndex < 0 || adjustedIndex >= submarine.PropulsionShaft.Count || submarine.PropulsionShaft[adjustedIndex].value <= 0)
                {
                    return false;
                }
            }
        }

        return true; // Both thrusters and shafts are correctly activated
    }
}
