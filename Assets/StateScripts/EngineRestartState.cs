using UnityEngine;

public class EngineRestartState : SubmarineBaseState
{
    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING:<br>Unexpected engine failure.";

        //change fuel and shaft and stave sensors
        //player has to look at these sensors, relay info to technician
        //technician will tell them to click sail conning tower and thrusters
    }
    public override void UpdateState(SubmarineStateManager submarine)
    {
        //if (player mistakes == 3) {
        //implode();
        // } else {
        //submarine.SwitchState(submarine.waterChangesState);
        //}
    }
}
