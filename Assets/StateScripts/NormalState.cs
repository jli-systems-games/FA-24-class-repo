using UnityEngine;

public class NormalState : SubmarineBaseState
{
    public override void EnterState(SubmarineStateManager submarine)
    {
        submarine.IssueText.text = "";

        //revert all stats to normal basic state
    }
    public override void UpdateState(SubmarineStateManager submarine)
    {
        submarine.SwitchState(submarine.depthControlState);
        //random time pass before switching to a new random state
    }
}
