using UnityEngine;

public class WaterChangesState : SubmarineBaseState
{
    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING:<br>Water shifts threatening stability.";

        //change water salinity, water temp, water density, buoyancy, and pressure sensors
        //player has to look at these sensors, relay info to technician
        //technician will tell them to click ballast tank, trim tank, vents, blow system, and heat exchangers
    }
    public override void UpdateState(SubmarineStateManager submarine)
    {
        //if (player mistakes == 3) {
        //implode();
        // } else {
        //end game
        //}
    }
}
