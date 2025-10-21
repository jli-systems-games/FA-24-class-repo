using UnityEngine;
using UnityEngine.UI;

public class LeakDetectionState : SubmarineBaseState
{
    private bool emergencyBlowActivated = false;
    private bool heatExchangersActivated = false;
    private bool midVentsHandled = false;
    private bool forwardOrAftVentsHandled = false;

    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING:<br>High-pressure leak in the hull.";
        Debug.Log("Leak Detection Issue");

        // Simulate sensor changes to reflect a leak scenario
        RandomizeSensors(submarine);

        // Reset progress
        emergencyBlowActivated = false;
        heatExchangersActivated = false;
        midVentsHandled = false;
        forwardOrAftVentsHandled = false;
    }

    public override void UpdateState(SubmarineStateManager submarine)
    {
        // Check for completion of all tasks
        if (emergencyBlowActivated && heatExchangersActivated && midVentsHandled && forwardOrAftVentsHandled)
        {
            Debug.Log("Leak contained successfully!");
            submarine.SwitchState(submarine.normalState); // Transition to next state
        }
    }

    public override void ChangeSensors(SubmarineStateManager submarine)
    {
        Time.timeScale = 0;
    }

    public override void ImplodeState(SubmarineStateManager submarine)
    {
        Debug.Log("Warning: SPARK!");
    }

    private void RandomizeSensors(SubmarineStateManager submarine)
    {
        submarine.PressureSensor.value = Random.Range(70, 100); // Simulating high-pressure leak
        submarine.WaterDensity.value = Random.Range(50, 90);
        submarine.BallastTankSensor[1].value = Random.Range(30, 70); // MID Ballast sensor
        submarine.TrimTankSensor[1].value = Random.Range(30, 70); // MID Trim sensor
    }

    public void HandlePressureAndWaterDensity(SubmarineStateManager submarine)
    {
        if (submarine.PressureSensor.value > 75 && submarine.WaterDensity.value > 60)
        {
            emergencyBlowActivated = true;
            heatExchangersActivated = true;
            Debug.Log("Emergency blow and heat exchangers activated successfully.");
        }
        else
        {
            Debug.LogWarning("Pressure or water density not in expected ranges. Check again.");
        }
    }

    public void HandleMidVents(SubmarineStateManager submarine)
    {
        if (submarine.BallastTankSensor[1].value > 60 && submarine.TrimTankSensor[1].value > 60)
        {
            OpenVents(submarine, new[] { 2, 10 }); // Open MID VENTS 2 and 10
            midVentsHandled = true;
            Debug.Log("Mid Vents 2 and 10 opened.");
        }
        else if (submarine.BallastTankSensor[1].value < 40 && submarine.TrimTankSensor[1].value < 40)
        {
            OpenVents(submarine, new[] { 5, 7 }); // Open MID VENTS 5 and 7
            midVentsHandled = true;
            Debug.Log("Mid Vents 5 and 7 opened.");
        }
        else
        {
            Debug.LogWarning("Mid ballast and trim tank sensors are not aligned. Check again.");
        }
    }

    public void HandleForwardOrAftVents(SubmarineStateManager submarine)
    {
        if (submarine.BallastTankSensor[1].value > submarine.TrimTankSensor[1].value)
        {
            OpenVents(submarine, new[] { 2, 3, 12 }); // Open FWD VENTS
            forwardOrAftVentsHandled = true;
            Debug.Log("Forward vents 2, 3, and 12 opened.");
        }
        else if (submarine.TrimTankSensor[1].value > submarine.BallastTankSensor[1].value)
        {
            OpenVents(submarine, new[] { 8, 11 }); // Open AFT VENTS
            forwardOrAftVentsHandled = true;
            Debug.Log("Aft vents 8 and 11 opened.");
        }
        else
        {
            Debug.LogWarning("Ballast and Trim Tank values are equal. Resolve discrepancy.");
        }
    }

    private void OpenVents(SubmarineStateManager submarine, int[] ventIndices)
    {
        foreach (int index in ventIndices)
        {
            submarine.VentToggles[index].isOn = true;
        }
    }
}
