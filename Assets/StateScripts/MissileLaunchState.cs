using UnityEngine;

public class MissileLaunchState : SubmarineBaseState
{
    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "WARNING:<br>Prepare and launch missiles.";

        //player has to load and launch vls missiles, does not need technician
    }
    public override void UpdateState(SubmarineStateManager submarine)
    {
        //if (player mistakes == 3) {
        //implode();
        // } else {
        //submarine.SwitchState(submarine.engineRestartState);
        //}
    }
}
