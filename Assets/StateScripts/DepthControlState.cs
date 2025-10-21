using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthControlState : SubmarineBaseState
{
    private float buoyancyChangeRate = 1f;
    private int[] correctVents;

    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING 0072:<br>Descending into unsafe levels.";

        RandomizeBallastTankValues(submarine.BallastTankSensor);

        submarine.BuoyantSensor.value = 50;
        submarine.PressureSensor.value = Random.Range(0f, 100f);
        submarine.WaterDensity.value = Random.Range(0f, 100f);
        submarine.WaterTemperature.value = Random.Range(0f, 100f);
        submarine.WaterSalinity.value = Random.Range(0f, 100f);
    }

    public override void UpdateState(SubmarineStateManager submarine)
    {
        ChangeSensors(submarine);

        DepthControlBallastStatus(submarine);

        if (CheckVents(submarine))
        {
            submarine.SwitchState(submarine.normalState);
        }
        else
        {
            //ImplodeState(submarine); // Trigger implode state if vents are incorrect
        }
    }

    public override void ChangeSensors(SubmarineStateManager submarine)
    {
        submarine.BuoyantSensor.value = Mathf.Clamp(submarine.BuoyantSensor.value + (buoyancyChangeRate * Time.deltaTime), 0, 100);
    }

    public override void ImplodeState(SubmarineStateManager submarine)
    {
        Debug.Log("Warning: SPARK!");
    }

    private void RandomizeBallastTankValues(List<Slider> ballastTankSensors)
    {
        foreach (Slider ballast in ballastTankSensors)
        {
            ballast.value = Random.Range(0f, 100f);
        }
    }

    private void DepthControlBallastStatus(SubmarineStateManager submarine)
    {
        if (submarine.BallastTankSensor[0].value > 60 || submarine.BallastTankSensor[0].value < 40)
        {
            ReferToPressureSensor(submarine);
        }
        if (submarine.BallastTankSensor[1].value > 60 || submarine.BallastTankSensor[1].value < 40)
        {
            ReferToWaterDensity(submarine);
        }
        if (submarine.BallastTankSensor[2].value > 60 || submarine.BallastTankSensor[2].value < 40)
        {
            ReferToWaterTemperature(submarine);
        }
        if (submarine.BallastTankSensor[3].value > 60 || submarine.BallastTankSensor[3].value < 40)
        {
            ReferToWaterSalinity(submarine);
        }
    }

    private void ReferToPressureSensor(SubmarineStateManager submarine)
    {
        if (submarine.PressureSensor.value > 60)
        {
            correctVents = new int[] { 3, 12 };
            Debug.Log("Technician: Open AFT VENTS 3 and 12.");
        }
        else if (submarine.PressureSensor.value < 40)
        {
            correctVents = new int[] { 5, 7 };
            Debug.Log("Technician: Open AFT VENTS 5 and 7.");
        }
    }

    private void ReferToWaterDensity(SubmarineStateManager submarine)
    {
        if (submarine.WaterDensity.value > 60)
        {
            correctVents = new int[] { 6, 11 };
            Debug.Log("Technician: Open AFT VENTS 6 and 11.");
        }
        else if (submarine.WaterDensity.value < 40)
        {
            correctVents = new int[] { 2, 8 };
            Debug.Log("Technician: Open AFT VENTS 2 and 8.");
        }
    }

    private void ReferToWaterTemperature(SubmarineStateManager submarine)
    {
        if (submarine.WaterTemperature.value > 60)
        {
            correctVents = new int[] { 28, 33 };
            Debug.Log("Technician: Open FWD VENTS 4 and 9.");
        }
        else if (submarine.WaterTemperature.value < 40)
        {
            correctVents = new int[] { 25, 34 };
            Debug.Log("Technician: Open FWD VENTS 1 and 10.");
        }
    }

    private void ReferToWaterSalinity(SubmarineStateManager submarine)
    {
        if (submarine.WaterSalinity.value > 60)
        {
            correctVents = new int[] { 27, 35 };
            Debug.Log("Technician: Open FWD VENTS 3 and 11.");
        }
        else if (submarine.WaterSalinity.value < 40)
        {
            correctVents = new int[] { 31, 32 };
            Debug.Log("Technician: Open FWD VENTS 7 and 8.");
        }
    }

    private bool CheckVents(SubmarineStateManager submarine)
    {

        bool ventsCorrect = true;

        foreach (int vent in correctVents)
        {
            if (vent - 1 >= 0 && vent - 1 < submarine.VentToggles.Count)
            {
                if (!submarine.VentToggles[vent - 1].isOn) 
                {
                    ventsCorrect = false;
                    break;
                }
            }
        }

        return ventsCorrect;
    }
}
