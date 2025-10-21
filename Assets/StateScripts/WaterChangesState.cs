using UnityEngine;
using UnityEngine.UI;

public class WaterChangesState : SubmarineBaseState
{
    private bool salinityHandled = false;
    private bool temperatureHandled = false;
    private bool densityHandled = false;

    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING:<br>Water shifts threatening stability.";
        Debug.Log("Water Changes Issue");

        // Randomize water-related sensor values to simulate instability
        RandomizeSensors(submarine);

        // Reset progress flags
        salinityHandled = false;
        temperatureHandled = false;
        densityHandled = false;
    }

    public override void UpdateState(SubmarineStateManager submarine)
    {
        // Check if all tasks are handled
        if (salinityHandled && temperatureHandled && densityHandled)
        {
            Debug.Log("Water shifts successfully stabilized.");
            submarine.SwitchState(submarine.normalState);
        }
    }

    public override void ChangeSensors(SubmarineStateManager submarine)
    {
    }

    public override void ImplodeState(SubmarineStateManager submarine)
    {
        Debug.Log("Warning: SPARK!");
    }

    private void RandomizeSensors(SubmarineStateManager submarine)
    {
        submarine.WaterSalinity.value = Random.Range(0, 100); // Simulate salinity changes
        submarine.WaterTemperature.value = Random.Range(0, 90); // Simulate temperature changes
        submarine.WaterDensity.value = Random.Range(0, 100); // Simulate density changes
        submarine.BuoyantSensor.value = Random.Range(0, 100);
        submarine.PressureSensor.value = Random.Range(0, 100);
    }

    public void HandleSalinityChanges(SubmarineStateManager submarine)
    {
        if (submarine.WaterSalinity.value > 75)
        {
            // Rapidly increasing salinity
            if (submarine.BuoyantSensor.value > 60)
            {
                OpenVents(submarine, new[] { 1, 7 }, "AFT");
                OpenVents(submarine, new[] { 3, 9 }, "MID");
                salinityHandled = true;
                Debug.Log("Handled salinity increase: Opened AFT VENTS 1 and 7, MID VENTS 3 and 9.");
            }
            else
            {
                OpenVents(submarine, new[] { 6, 8 }, "MID");
                OpenVents(submarine, new[] { 4, 10 }, "FWD");
                salinityHandled = true;
                Debug.Log("Handled salinity decrease: Opened MID VENTS 6 and 8, FWD VENTS 4 and 10.");
            }
        }
        else if (submarine.WaterSalinity.value < 30)
        {
            // Rapidly decreasing salinity
            if (submarine.PressureSensor.value > 60)
            {
                OpenVents(submarine, new[] { 2, 7 }, "AFT");
                OpenVents(submarine, new[] { 5, 6 }, "FWD");
                salinityHandled = true;
                Debug.Log("Handled salinity decrease: Opened AFT VENTS 2 and 7, FWD VENTS 5 and 6.");
            }
            else
            {
                OpenVents(submarine, new[] { 8, 11 }, "AFT");
                OpenVents(submarine, new[] { 1, 3 }, "MID");
                salinityHandled = true;
                Debug.Log("Handled salinity decrease: Opened AFT VENTS 8 and 11, MID VENTS 1 and 3.");
            }
        }
    }

    public void HandleTemperatureChanges(SubmarineStateManager submarine)
    {
        if (submarine.WaterTemperature.value > 70)
        {
            // Rapidly increasing temperature
            if (submarine.PressureSensor.value > 60)
            {
                OpenVents(submarine, new[] { 3, 5 }, "AFT");
                OpenVents(submarine, new[] { 7, 12 }, "MID");
                temperatureHandled = true;
                Debug.Log("Handled temperature increase: Opened AFT VENTS 3 and 5, MID VENTS 7 and 12.");
            }
            else
            {
                OpenVents(submarine, new[] { 10, 11 }, "AFT");
                OpenVents(submarine, new[] { 8, 10 }, "FWD");
                temperatureHandled = true;
                Debug.Log("Handled temperature increase: Opened AFT VENTS 10 and 11, FWD VENTS 8 and 10.");
            }
        }
        else if (submarine.WaterTemperature.value < 30)
        {
            // Rapidly decreasing temperature
            if (submarine.BuoyantSensor.value > 60)
            {
                OpenVents(submarine, new[] { 4, 8 }, "MID");
                OpenVents(submarine, new[] { 2, 12 }, "FWD");
                temperatureHandled = true;
                Debug.Log("Handled temperature decrease: Opened MID VENTS 4 and 8, FWD VENTS 2 and 12.");
            }
            else
            {
                OpenVents(submarine, new[] { 5, 7 }, "AFT");
                OpenVents(submarine, new[] { 3, 9 }, "FWD");
                temperatureHandled = true;
                Debug.Log("Handled temperature decrease: Opened AFT VENTS 5 and 7, FWD VENTS 3 and 9.");
            }
        }
    }

    public void HandleDensityChanges(SubmarineStateManager submarine)
    {
        if (submarine.WaterDensity.value > 65)
        {
            // Rapidly increasing density
            if (submarine.BuoyantSensor.value > 60)
            {
                OpenVents(submarine, new[] { 7, 11 }, "MID");
                OpenVents(submarine, new[] { 8, 10 }, "FWD");
                densityHandled = true;
                Debug.Log("Handled density increase: Opened MID VENTS 7 and 11, FWD VENTS 8 and 10.");
            }
            else
            {
                OpenVents(submarine, new[] { 1, 5 }, "AFT");
                OpenVents(submarine, new[] { 3, 6 }, "MID");
                densityHandled = true;
                Debug.Log("Handled density increase: Opened AFT VENTS 1 and 5, MID VENTS 3 and 6.");
            }
        }
        else if (submarine.WaterDensity.value < 30)
        {
            // Rapidly decreasing density
            if (submarine.PressureSensor.value > 60)
            {
                OpenVents(submarine, new[] { 10, 4 }, "AFT");
                OpenVents(submarine, new[] { 4, 9 }, "FWD");
                densityHandled = true;
                Debug.Log("Handled density decrease: Opened AFT VENTS 10 and 4, FWD VENTS 4 and 9.");
            }
            else
            {
                OpenVents(submarine, new[] { 2, 12 }, "MID");
                OpenVents(submarine, new[] { 2, 11 }, "FWD");
                densityHandled = true;
                Debug.Log("Handled density decrease: Opened MID VENTS 2 and 12, FWD VENTS 2 and 11.");
            }
        }
    }

    private void OpenVents(SubmarineStateManager submarine, int[] ventIndices, string ventSection)
    {
        foreach (int index in ventIndices)
        {
            submarine.VentToggles[index].isOn = true;
            Debug.Log($"{ventSection} Vent {index} opened.");
        }
    }
}
