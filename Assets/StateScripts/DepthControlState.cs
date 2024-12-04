using UnityEngine;

public class DepthControlState : SubmarineBaseState
{
    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING:<br>Descending below safe limits.";

        //change buoyant, ballast tank, water density, pressure sensors
        //player has to look at these sensors, relay info to technician
        //technician will tell them to click vents, blow system, and heat exchangers
    }
    public override void UpdateState(SubmarineStateManager submarine)
    {
        //if (player mistakes == 3) {
        //implode();
        // } else {
        //submarine.SwitchState(submarine.pressureCalibrationState);
        //}
    }
}
