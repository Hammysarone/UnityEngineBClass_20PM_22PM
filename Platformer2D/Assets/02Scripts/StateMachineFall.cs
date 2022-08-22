using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineFall : StateMachineBase
{
    private GroundDetecter _groundDetecter;
    public StateMachineFall(StateMachineManager.State machineState, 
                            StateMachineManager manager, 
                            AnimationManager animationManager) 
        : base(machineState, manager, animationManager)
    {
        _groundDetecter = manager.GetComponent<GroundDetecter>();
    }

    public override void Execute()
    {
        manager.isMovable = false;
        manager.isDirectionChangable = true;
        state = State.Prepare;
    }

    public override void FixedUpdateState()
    {

    }

    public override void ForceStop()
    {
        state = State.Idle;
    }

    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if(_groundDetecter.isDetected == false &&
           manager.state == StateMachineManager.State.Idle ||
           manager.state == StateMachineManager.State.Move ||
           manager.state == StateMachineManager.State.Jump ||
           manager.state == StateMachineManager.State.DownJump)
            isOK = true;
        return isOK;
    }

    public override StateMachineManager.State UpdateState()
    {
        StateMachineManager.State nextState = machineState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animationManager.Play("Fall");
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if(_groundDetecter.isDetected)
                {
                    state++;
                }
                break;
            case State.Finish:
                nextState = StateMachineManager.State.Idle;
                break;
            case State.Error:
                break;
            case State.WaitForErrorClear:
                break;
            default:
                break;
        }
        return nextState;
    }
}
