using UnityEngine;

public abstract class SubmarineBaseState
{
    public abstract void EnterState(SubmarineStateManager submarine);
    public abstract void UpdateState(SubmarineStateManager submarine);
    public abstract void ChangeSensors(SubmarineStateManager submarine);
    public abstract void ImplodeState(SubmarineStateManager submarine);
}
