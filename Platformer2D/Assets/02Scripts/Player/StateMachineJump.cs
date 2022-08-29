using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineJump : StateMachineBase
{
    private GroundDetecter _groundDetecter;
    private Rigidbody2D _rb;
    private float _jumpForce = 3.0f;
    private float _downJumpForce = 1.5f;
    private bool _isDownJump;
    public StateMachineJump(StateMachineManager.State machineState,
                            StateMachineManager manager, 
                            AnimationManager animationManager) 
        : base(machineState, manager, animationManager)
    {
        shortKey = KeyCode.Z;
        _groundDetecter = manager.GetComponent<GroundDetecter>();
        _rb = manager.GetComponent<Rigidbody2D>();
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
        _isDownJump = false;
        state = State.Idle;
    }

    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if (_groundDetecter.isDetected)
        {
            if(manager.state == StateMachineManager.State.Crouch)
            {
                _isDownJump = true;
                isOK = true;
            }
            else if (manager.state != StateMachineManager.State.Jump &&
                     manager.state != StateMachineManager.State.Fall)
            {
                _isDownJump = false;
                isOK = true;
            }
        }

        return isOK;
    }

    public override StateMachineManager.State UpdateState()
    {
        if (_isDownJump)
            return DownJumpWorkFlow();
        else
            return NormalJumpWorkFlow();
    }

    private StateMachineManager.State NormalJumpWorkFlow()
    {
        StateMachineManager.State nextState = managerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animationManager.Play("Jump");
                _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                state++;
                break;
            case State.Casting:
                if (_groundDetecter.isDetected == false)
                {
                    state++;
                }
                break;
            case State.OnAction:
                if (_rb.velocity.y < 0.0f ||
                    _groundDetecter.isDetected == true)
                {
                    state++;
                }
                break;
            case State.Finish:
                if(_groundDetecter.isDetected == true)
                {
                    nextState = StateMachineManager.State.Idle;
                }
                else
                {
                    nextState = StateMachineManager.State.Fall;
                }
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

    private StateMachineManager.State DownJumpWorkFlow()
    {
        StateMachineManager.State nextState = managerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animationManager.Play("Jump");
                _groundDetecter.IgnoreLastGround();
                state++;
                break;
            case State.Casting:
                _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                _rb.AddForce(Vector2.up * _downJumpForce, ForceMode2D.Impulse);
                state++;
                break;
            case State.OnAction:
                if (_rb.velocity.y < 0.0f)
                {
                    animationManager.Play("Fall");
                }

                if(_groundDetecter.isIgnoringGround ||
                   _groundDetecter.isGroundChanged)
                {
                    state++;
                }
                break;
            case State.Finish:
                if (_groundDetecter.isDetected)
                    nextState = StateMachineManager.State.Idle;
                else
                    nextState = StateMachineManager.State.Fall;
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
