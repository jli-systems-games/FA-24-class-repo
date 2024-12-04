using UnityEngine;

public class NavigationHazardState : SubmarineBaseState
{
    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING:<br>Rapidly approaching underwater obstacle.";

        //change fuel, shaft and stave, trim, ballast, and gps and nav sensors
        //player has to look at these sensors, relay info to technician
        //technician will tell them to click sail conning tower, sonar array, vents, heat exchangers, blow system, and thrusters
    }
    public override void UpdateState(SubmarineStateManager submarine)
    {
        //if (player mistakes == 3) {
        //implode();
        // } else {
        //submarine.SwitchState(submarine.missileLaunchStae);
        //}
    }
}
