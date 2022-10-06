using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineEdgeGrab : StateMachineBase
{
    private enum EdgeType
    {
        EdgeGrab,
        EdgeIdle,
        EdgeClimb,
        EdgeSlide
    }

    private EdgeType _edgeType;
    private EdgeDetector _edgeDetector;
    private GroundDetecter _groundDetector;
    private Rigidbody2D _rb;
    private float _edgeGrabAnimationTime;
    private float _edgeClimbAnimationTime;
    private float _animationTimer;
    private Vector2 _slerpCenter;
    private Vector2 _grabPos;
    private Vector2 _climbPos;
    private float _slideSpeed = 10.0f;

    public StateMachineEdgeGrab(StateMachineManager.State machineState, 
                                StateMachineManager manager, 
                                AnimationManager animationManager) 
        : base(machineState, manager, animationManager)
    {
        _edgeDetector = manager.GetComponent<EdgeDetector>();
        _groundDetector = manager.GetComponent<GroundDetecter>();
        _rb = manager.GetComponent<Rigidbody2D>();
        _edgeGrabAnimationTime = animationManager.GetAnimationTime("EdgeGrab");
        _edgeClimbAnimationTime = animationManager.GetAnimationTime("EdgeClimb");
    }

    public override void Execute()
    {
        manager.isMovable = false;
        manager.isDirectionChangable = false;
        manager.ResetVelocity();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _edgeType = EdgeType.EdgeGrab;
        state = State.Prepare;
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ForceStop()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        state = State.Idle;
    }

    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if (_edgeDetector.isDetected)
            isOK = true;
        return isOK;
    }

    public override StateMachineManager.State UpdateState()
    {
        StateMachineManager.State nextState = managerState;

        switch (_edgeType)
        {
            case EdgeType.EdgeGrab:
                return EdgeGrabWorkFlow();
            case EdgeType.EdgeIdle:
                return EdgeIdleWorkFlow();
            case EdgeType.EdgeClimb:
                return EdgeClimbWorkFlow();
            case EdgeType.EdgeSlide:
                return EdgeSlideWorkFlow();
            default:
                throw new System.Exception("Edge Grab ���� ���� �߰�");
        }
    }

    private StateMachineManager.State EdgeGrabWorkFlow()
    {
        StateMachineManager.State nextState = managerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animationManager.Play("EdgeGrab");
                _grabPos = _edgeDetector.grabPos;
                _climbPos = _edgeDetector.climbPos;
                _animationTimer = _edgeClimbAnimationTime;
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if (_animationTimer < 0.0f)
                {
                    state++;
                }
                else
                {
                    _animationTimer -= Time.deltaTime;
                }
                break;
            case State.Finish:
                _edgeType = EdgeType.EdgeIdle;
                state = State.Prepare;
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

    private StateMachineManager.State EdgeIdleWorkFlow()
    {
        StateMachineManager.State nextState = managerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animationManager.Play("EdgeIdle");
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _edgeType = EdgeType.EdgeClimb;
                    state = State.Prepare;
                }
                else if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _edgeType = EdgeType.EdgeSlide;
                    state = State.Prepare;
                }
                break;
            case State.Finish:
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

    private StateMachineManager.State EdgeClimbWorkFlow()
    {
        StateMachineManager.State nextState = managerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animationManager.Play("EdgeClimb");
                _animationTimer = _edgeClimbAnimationTime;
                _slerpCenter = new Vector2(_edgeDetector.climbPos.x * 2.0f - _rb.position.x,
                                           _rb.position.y * 2.0f - _edgeDetector.climbPos.y);
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if(_animationTimer < 0)
                {

                    state = State.Finish;
                }
                else
                {
                    if (_rb.position.y < _grabPos.y)
                    {
                        _rb.position += Vector2.up * Time.deltaTime / _edgeClimbAnimationTime;
                    }
                    else if(Mathf.Abs(_rb.position.x - _climbPos.x) > 0.01f)
                    {
                        _rb.position += Vector2.right * manager.direction * Time.deltaTime / _edgeClimbAnimationTime;
                    }
                    _animationTimer -= Time.deltaTime;
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

    private StateMachineManager.State EdgeSlideWorkFlow()
    {
        StateMachineManager.State nextState = managerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animationManager.Play("EdgeSlide");
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if(_groundDetector.isDetected)
                {
                    state = State.Finish;
                }
                else
                {
                    _rb.MovePosition(_rb.position + Vector2.down * _slideSpeed * Time.deltaTime);
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