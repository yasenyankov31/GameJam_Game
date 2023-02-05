using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLocomotion : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private InputController input = null;
    private Rigidbody2D _body;
    private CollisionDataRetriever _ground;
    public BoxCollider2D DestroyZone;
    public Animator playerAnimator;
    public int LevelIndex;

    [Header("Movement")]
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    private Vector2 _direction, _desiredVelocity, _velocity;
    private float _maxSpeedChange, _acceleration;
    private bool isFacingRight = true;
    public Animator animator;
    public SpriteRenderer render;

    [Header("Jumping")]
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;

    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed;

    private bool _desiredJump, _onGround;

    [Header("Wall sliding and Wall jumping")]
    public bool isWallSliding, isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private float wallSlidingSpeed = 2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;



    [Header("Grab and Throw")]
    private GrabAndThrow shootScript;

    public bool isMonkey, isHuman=true, isBarbarian,isAlive=true;

    

    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        shootScript = GetComponentInChildren<GrabAndThrow>();
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<CollisionDataRetriever>();

        _defaultGravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            AnimationsFlags();
            JumpInput();
            MovementInput();
            if (isMonkey)
            {
                WallSlide();
                WallJump();
            }

            if (!isWallJumping)
            {
                Flip();
            }


        }
        var shouldReset = animator.GetBool("resetPosition");
        if (shouldReset)
        {
            Revive();
        }

    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            JumpLogic();
            if (!isWallJumping)
            {
                MovementLogic();
            }
        }

    }

    #region Movement
    private void MovementInput()
    {
        _direction.x = input.RetieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.GetFriction(), 0f);
    }

    private void MovementLogic()
    {
        _onGround = _ground.GetOnGround();
        _velocity = _body.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _body.velocity = _velocity;
    }
    public void Flip()
    {
        if (isFacingRight && input.RetieveMoveInput() < 0f || !isFacingRight
            && input.RetieveMoveInput() > 0f)
        {
            //shootScript.direction.x *= -1f;
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    #endregion

    #region Jumping

    private void JumpInput()
    {
        _desiredJump |= input.RetieveJumpInput();
    }
    private void JumpLogic()
    {
        _onGround = _ground.GetOnGround();
        _velocity = _body.velocity;

        if (_onGround)
        {
            playerAnimator.SetBool("isJumping", false);
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if (_body.velocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if (_body.velocity.y < 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }
        else if (_body.velocity.y == 0)
        {
            _body.gravityScale = _defaultGravityScale;
        }

        _body.velocity = _velocity;
    }

    private void JumpAction()
    {
        
        if (_onGround || _jumpPhase < _maxAirJumps)
        {
            _jumpPhase++;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
            if (_velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - _velocity.y, 0f);
            }
            _velocity.y = jumpSpeed;
        }
    }
    #endregion

    #region Wall Sliding and Wall Jumping
    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !_ground.onGround)
        {
            isWallSliding = true;
            _body.velocity = new Vector2(_body.velocity.x, Mathf.Clamp(_body.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (input.RetieveJumpInput() && wallJumpingCounter > 0)
        {
            isWallJumping = true;
            _body.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            if (transform.localScale.x != wallJumpingDirection)
            {
                //shootScript.direction.x *= -1f;
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

    }
    private void StopWallJumping()
    {
        isWallJumping = false;

    }
    #endregion

    #region Animation
    private void AnimationsFlags()
    {
        if (input.RetieveJumpInput())
        {
            playerAnimator.SetBool("isJumping", true);
        }
        if (input.RetieveMoveInput()!=0)
        {
            playerAnimator.SetBool("isMoving",true);
        }
        else
        {
            playerAnimator.SetBool("isMoving",false);
        }
        
    }

    #endregion

    #region Activity

    public void Die()
    {
        isAlive = false;
        _body.bodyType = RigidbodyType2D.Static;
        animator.SetBool("isAlive", false);
        render.gameObject.SetActive(false);
    }

    public void Revive()
    {
        SceneManager.LoadScene(LevelIndex);
    }

    #endregion
}
