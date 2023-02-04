using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;

    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private CollisionDataRetriever _ground;

    private float _maxSpeedChange, _acceleration;
    private bool _onGround;
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<CollisionDataRetriever>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = input.RetieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x,0f)*Mathf.Max(_maxSpeed-_ground.GetFriction(),0f);
    }

    private void FixedUpdate()
    {
        _onGround = _ground.GetOnGround();
        _velocity = _body.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration*Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x,_desiredVelocity.x,_maxSpeedChange);

        _body.velocity = _velocity;
    }
}
