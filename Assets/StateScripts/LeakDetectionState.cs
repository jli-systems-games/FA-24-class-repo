using UnityEngine;

public class LeakDetectionState : SubmarineBaseState
{
    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING:<br>High-pressure leak in the hull.";

        //change pressure, buoyancy, trim, balalst, water density sensors
        //player has to look at these sensors, relay info to technician
        //technician will tell them to click vents, blow system, and heat exchangers
    }
    public override void UpdateState(SubmarineStateManager submarine)
    {
        //if (player mistakes == 3) {
        //implode();
        // } else {
        //submarine.SwitchState(submarine.navigationHazardState);
        //}
    }
}
