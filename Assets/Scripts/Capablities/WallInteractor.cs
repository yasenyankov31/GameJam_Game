using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteractor : MonoBehaviour
{
    public bool isWallJumping;
    [Header("Wall Slide")]
    [SerializeField] [Range(0.1f, 5f)] private float _wallSlideMaxSpeed = 2f;
    [Header("Wall Jump")]
    [SerializeField] private Vector2 _wallJumpLeap = new Vector2(14f,12f);

    private CollisionDataRetriever _collisionDataRetriever;
    private Rigidbody2D _body;
    private Controller _controller;

    private Vector2 _velocity;
    private bool _onWall,_onGround,_desieredJump;
    private float _wallDirectionX;

    // Start is called before the first frame update
    void Start()
    {
        _collisionDataRetriever = GetComponent<CollisionDataRetriever>();
        _body = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        _velocity = _body.velocity;
        _onWall = _collisionDataRetriever.onWall;
        _onGround = _collisionDataRetriever.onGround;
        _wallDirectionX = _collisionDataRetriever.ContactNormal.x;

        #region Wall Slide
        if (_onWall)
        {
            if (_velocity.y<-_wallSlideMaxSpeed)
            {
                _velocity.y = -_wallSlideMaxSpeed;

            }
        }
        #endregion
        if ( _onWall && _controller.input.RetieveJumpInput())
        {
            isWallJumping = true;
            _velocity = new Vector2(_wallJumpLeap.x * _wallDirectionX,_wallJumpLeap.y);
        }

        _body.velocity = _velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionDataRetriever.EvaluateCollision(collision);
        if (_collisionDataRetriever.onWall && !_collisionDataRetriever.onGround && isWallJumping)
        {
            _body.velocity = Vector2.zero;
        }
    }
}
