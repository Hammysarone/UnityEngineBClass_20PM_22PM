using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Jump,
        Fall,
        Attack,
        Dash,
        Slide
    }

    private enum IdleState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    private enum MoveState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    private enum JumpState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    private enum FallState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    private enum SlideState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    private enum AttackState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    private enum DashState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    public State state;
    [SerializeField] private MoveState   _moveState;
    [SerializeField] private IdleState   _idleState;
    [SerializeField] private JumpState   _jumpState;
    [SerializeField] private FallState   _fallState;
    [SerializeField] private SlideState  _slideState;
    [SerializeField] private AttackState _attackState;
    [SerializeField] private DashState   _dashState;

    private Vector2 _move;
    [SerializeField] private float _moveSpeed  = 1.0f;
    [SerializeField] private float _jumpForce  = 5.0f;
    [SerializeField] private float _slideSpeed = 3.0f;
    [SerializeField] private float _dashSpeed  = 4.0f;

    [SerializeField] private Vector2 _attackHitCastCenter;
    [SerializeField] private Vector2 _attackHitCastSize;
    [SerializeField] private LayerMask _attackTargetLayer;

    private int _direction;

    // -1 : left, +1 : right
    public int direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (value < 0)
            {
                _direction = -1;
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
            else
            {
                _direction = 1;
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
    [SerializeField] private int _directionInit;
    private Animator _animator;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private GroundDetecter _groundDetecter;
    private Vector2 _colSizeOrigin;
    private Vector2 _colOffsetOrigin;
    [SerializeField] private Vector2 _colOffsetCrouch;
    [SerializeField] private Vector2 _colSizeCrouch;

    private bool isMovable = true;
    private bool isDirenctionChangable = true;
    private float _slideAnimationTime;
    private float _attackAnimationTime;
    private float _dashAnimationTime;
    private float _animationTimer;

    private float h { get => Input.GetAxis("Horizontal"); }
    private float v { get => Input.GetAxis("Vertical"); }

    private void Awake()
    {
        direction = _directionInit;
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _groundDetecter = GetComponent<GroundDetecter>();
        _colOffsetOrigin = _col.offset;
        _colSizeOrigin = _col.size;
        _slideAnimationTime = GetAnimationTime("Slide");
        _attackAnimationTime = GetAnimationTime("Attack");
        _dashAnimationTime = GetAnimationTime("Dash");
    }

    private void Update()
    {
        if(isDirenctionChangable)
        { 
            if (h < 0.0f)
                direction = -1;
            else if (h > 0.0f)
                direction = 1;
        }

        
        if(isMovable)
        {
            _move.x = h;
            if (Mathf.Abs(_move.x) > 0.0f)
                ChangeState(State.Move);
            else
                ChangeState(State.Idle);
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Z) && 
            state != State.Jump &&
            state != State.Fall)
        {
            ChangeState(State.Jump);
        }

        // 슬라이드
        if (Input.GetKeyDown(KeyCode.LeftControl) &&
            (state == State.Idle || state == State.Move))
        {

            ChangeState(State.Slide);
        }

        // 공격
        if (Input.GetKeyDown(KeyCode.X) &&
            (state == State.Idle ||
             state == State.Move ||
             state == State.Jump ||
             state == State.Fall))
        {
            ChangeState(State.Attack);
        }

        // 대쉬
        if(Input.GetKeyDown(KeyCode.C) &&
            (state == State.Idle ||
            state == State.Move ||
            state == State.Jump ||
            state == State.Fall))
        {
            ChangeState(State.Dash);
        }

            UpdateState();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(_move.x * _moveSpeed, _move.y, 0.0f) * Time.fixedDeltaTime;
    }

    private void UpdateState()
    {
        switch (state)
        {
            case State.Idle:
                UpdateIdleState();
                break;
            case State.Move:
                UpdateMoveState();
                break;
            case State.Jump:
                UpdateJumpState();
                break;
            case State.Fall:
                UpdateFallState();
                break;
            case State.Attack:
                UpdateAttackState();
                break;
            case State.Dash:
                UpdateDashState();
                break;
            case State.Slide:
                UpdateSlideState();
                break;
            default:
                break;
        }
    }

    private void ChangeState(State newState)
    {
        if (state == newState)
            return;

        // 이전 하위 상태 머신 초기화
        switch (state)
        {
            case State.Idle:
                _idleState = IdleState.Idle;
                break;
            case State.Move:
                _moveState = MoveState.Idle;
                break;
            case State.Jump:
                _jumpState = JumpState.Idle;
                break;
            case State.Fall:
                _fallState = FallState.Idle;
                break;
            case State.Attack:
                _attackState = AttackState.Idle;
                break;
            case State.Dash:
                _dashState = DashState.Idle;
                break;
            case State.Slide:
                _slideState = SlideState.Idle;
                _col.offset = _colOffsetOrigin;
                _col.size = _colSizeOrigin;
                break;
            default:
                break;
        }

        // 다음 하위 상태 머신 준비
        switch (newState)
        {
            case State.Idle:
                _idleState = IdleState.Prepare;
                break;
            case State.Move:
                _moveState = MoveState.Prepare;
                break;
            case State.Jump:
                _jumpState = JumpState.Prepare;
                break;
            case State.Fall:
                _fallState = FallState.Prepare;
                break;
            case State.Attack:
                _attackState = AttackState.Prepare;
                break;
            case State.Dash:
                _dashState = DashState.Prepare;
                break;
            case State.Slide:
                _slideState = SlideState.Prepare;
                _col.offset = _colOffsetCrouch;
                _col.size = _colSizeCrouch;
                break;
            default:
                break;
        }

        state = newState;
    }

    private void UpdateIdleState()
    {
        switch (_idleState)
        {
            case IdleState.Idle:
                break;
            case IdleState.Prepare:
                isMovable = true;
                isDirenctionChangable = true;
                _animator.Play("Idle");
                _idleState = IdleState.OnAction;
                break;
            case IdleState.Casting:
                break;
            case IdleState.OnAction:
                // nothing to do
                break;
            case IdleState.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateMoveState()
    {
        switch (_moveState)
        {
            case MoveState.Idle:
                break;
            case MoveState.Prepare:
                isMovable = true;
                isDirenctionChangable = true;
                _animator.Play("Move");
                _moveState = MoveState.OnAction;
                break;
            case MoveState.Casting:
                break;
            case MoveState.OnAction:
                // nothing to do;
                break;
            case MoveState.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateJumpState()
    {
        switch (_jumpState)
        {
            case JumpState.Idle:
                break;
            case JumpState.Prepare:
                isMovable = false;
                isDirenctionChangable = true;
                _animator.Play("Jump");
                _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _jumpState++;
                break;
            case JumpState.Casting:
                // 발이 땅에서 떨어졌는지
                if (_groundDetecter.isDetected == false)
                    _jumpState++;
                break;
            case JumpState.OnAction:
                if (_rb.velocity.y < 0)
                {
                    ChangeState(State.Fall);
                }
                break;
            case JumpState.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateFallState()
    {
        switch (_fallState)
        {
            case FallState.Idle:
                break;
            case FallState.Prepare:
                isMovable = false;
                isDirenctionChangable = true;
                _animator.Play("Fall");
                _fallState = FallState.OnAction;
                break;
            case FallState.Casting:
                break;
            case FallState.OnAction:
                if(_groundDetecter.isDetected)
                {
                    ChangeState(State.Idle);
                }
                break;
            case FallState.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateSlideState()
    {
        switch (_slideState)
        {
            case SlideState.Idle:
                break;
            case SlideState.Prepare:
                isMovable = false;
                isDirenctionChangable = false;
                _animator.Play("Slide");
                _animationTimer = _slideAnimationTime;
                _slideState++;
                break;
            case SlideState.Casting:
                if (_animationTimer < _slideAnimationTime * 3.0f / 4.0f)
                    _slideState++;
                else
                {
                    _rb.velocity = Vector2.right * direction * _moveSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            case SlideState.OnAction:
                if (_animationTimer < _slideAnimationTime * 1.0f / 4.0f)
                    _slideState++;
                else
                {
                    _rb.velocity = Vector2.right * _direction * _slideSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            case SlideState.Finish:
                if(_animationTimer < 0.0f)
                {
                    ChangeState(State.Idle);
                }
                else
                {
                    _rb.velocity = Vector2.right * _direction * _slideSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            default:
                break;
        }
    }

    private void UpdateAttackState()
    {
        switch (_attackState)
        {
            case AttackState.Idle:
                break;
            case AttackState.Prepare:
                isMovable = false;
                isDirenctionChangable = false;
                _animator.Play("Attack");
                _animationTimer = _attackAnimationTime;
                _attackState = AttackState.OnAction;
                break;
            case AttackState.Casting:
                break;
            case AttackState.OnAction:
                if(_animationTimer < 0)
                {
                    ChangeState(State.Idle);
                }
                _animationTimer -= Time.deltaTime;
                break;
            case AttackState.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateDashState()
    {
        switch (_dashState)
        {
            case DashState.Idle:
                break;
            case DashState.Prepare:
                isMovable = false;
                isDirenctionChangable = false;
                _animator.Play("Dash");
                _animationTimer = _dashAnimationTime;
                _dashState++;
                break;
            case DashState.Casting:
                if (_animationTimer < _dashAnimationTime * 3.5f / 4.0f)
                    _dashState++;
                else
                {
                    _rb.velocity = Vector2.right * direction * _moveSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            case DashState.OnAction:
                if (_animationTimer < _dashAnimationTime * 1.5f / 4.0f)
                    _dashState++;
                else
                {
                    _rb.velocity = Vector2.right * _direction * _dashSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            case DashState.Finish:
                if (_animationTimer < 0.0f)
                {
                    ChangeState(State.Idle);
                }
                else
                {
                    _rb.velocity = Vector2.right * _direction * _moveSpeed;
                }
                _animationTimer -= Time.deltaTime;
                break;
            default:
                break;
        }
    }

    private float GetAnimationTime(string clipName)
    {
        RuntimeAnimatorController rac = _animator.runtimeAnimatorController;
        for(int i = 0; i < rac.animationClips.Length; i++)
        {
            if(rac.animationClips[i].name == clipName)
            {
                return rac.animationClips[i].length;
            }
        }

        Debug.Log($"{clipName}을 찾지 못함");
        return -1.0f;
    }

    private void AttackHit()
    {
        Vector2 attackCenter = new Vector2(_attackHitCastCenter.x * _direction,
                                           _attackHitCastCenter.y) + _rb.position;

        RaycastHit2D hit = Physics2D.BoxCast(attackCenter,
                          _attackHitCastSize,
                          0.0f,
                          Vector2.zero, 
                          0.0f, 
                          _attackTargetLayer);

        if(hit.collider != null)
        {
            Debug.Log($"Attack Hit ! : {hit.collider.gameObject.name}");
        }
    }

    private void OnDrawGizmos()
    {
        if (_rb == null)
            return;

        Vector2 attackCenter = new Vector2(_attackHitCastCenter.x * _direction,
                                           _attackHitCastCenter.y) + _rb.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackCenter, _attackHitCastSize);
    }
}