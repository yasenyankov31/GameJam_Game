using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;

    private Rigidbody2D _body;
    private CollisionDataRetriever _ground;
    private Vector2 _velocity;

    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed;

    private bool _desiredJump, _onGround;
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<CollisionDataRetriever>();

        _defaultGravityScale = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        _desiredJump |= input.RetieveJumpInput();
    }

    private void FixedUpdate()
    {
        _onGround = _ground.GetOnGround();
        _velocity = _body.velocity;

        if (_onGround)
        {
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
            float jumpSpeed = Mathf.Sqrt(-2f*Physics2D.gravity.y * _jumpHeight);
            if (_velocity.y> 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed- _velocity.y,0f);
            }
            _velocity.y = jumpSpeed;
        }
    }
}
