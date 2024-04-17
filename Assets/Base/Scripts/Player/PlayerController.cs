using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies"), SerializeField]
    private CharacterController _charController;

    [SerializeField]
    private Transform _meshBody;

    [SerializeField]
    private MovementStats _moveStats;

    [SerializeField]
    private Animator _anim;

    [Header("Particles"), SerializeField]
    private ParticleSystem _runParticle;
    
    [SerializeField]
    private ParticleSystem _jumpParticle;
    
    private Vector2 _currentHorizontalVelocity;
    private float _yVelocity;

    private int _animState = Animator.StringToHash("State");
    
    private bool _wasGrounded;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector2 input = new Vector2(horizontal, vertical);
        
        
        HandleHorizontalMovement(input);

        HandleVerticalMovement();
        
        HandleAnimation(input);
    }

    private void HandleHorizontalMovement(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        
        // rotate moveDirection to face camera
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        
        // project onto the xz plane
        moveDirection.y = 0;
        moveDirection.Normalize();
        
        Vector2 targetHorizontalVelocity = new Vector2(moveDirection.x, moveDirection.z) * _moveStats.moveSpeed;

        if (moveDirection.magnitude > 0)
        {
            _currentHorizontalVelocity = Vector2.MoveTowards(_currentHorizontalVelocity, targetHorizontalVelocity, _moveStats.moveAccel * Time.deltaTime);
        }
        else
        {
            _currentHorizontalVelocity = Vector2.MoveTowards(_currentHorizontalVelocity, Vector2.zero, _moveStats.moveFriction * Time.deltaTime);
        }
        
        _wasGrounded = _charController.isGrounded;
        
        // Move!
        _charController.Move(new Vector3(_currentHorizontalVelocity.x, _yVelocity, _currentHorizontalVelocity.y) * Time.deltaTime);

        
        var velocity = _charController.velocity;
        _currentHorizontalVelocity = new Vector2(velocity.x, velocity.z);
    }

    private void HandleVerticalMovement()
    {
        // Gravity
        _yVelocity += Physics.gravity.y * Time.deltaTime;

        if( _charController.isGrounded && _yVelocity < 0)
        {
            _yVelocity = -10f;
        }
        
        // limit fall speed
        
        if(!_charController.isGrounded && _wasGrounded)
            _yVelocity = Mathf.Max(_yVelocity, 0);
        
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && _charController.isGrounded)
        {
            _jumpParticle.Play();
            _yVelocity = _moveStats.jumpVelocity;
        }
    }
    
    
    private void HandleAnimation(Vector2 input)
    {
        // Smoothly rotate player to face movement direction
        if (input.magnitude > 0)
        {
            var currentRotation = _meshBody.rotation;
            var horizontalVel = new Vector3(input.x, 0, input.y);
            var targetRotation = Quaternion.LookRotation(horizontalVel);
            
            float t = 1 - Mathf.Pow(1 - 0.999f, Time.deltaTime);
            _meshBody.rotation = Quaternion.Lerp(currentRotation, targetRotation, t);
        }
        
        // Animation
        int finalState = 0;
        if (_charController.isGrounded)
        {
            finalState = input.magnitude > 0 ? 1 : 0;
        }
        else
        {
            finalState = 2;
        }
        
        _anim.SetInteger(_animState, finalState);
        
        // Particles
        if(finalState == 1)
        {
            if (!_runParticle.isPlaying)
            {
                _runParticle.Play();
            }
        }
        else
        {
            if (_runParticle.isPlaying)
            {
                _runParticle.Stop();
            }
        }

    }

    public void Launch(float jumpHeight)
    {
        _yVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
    }
}
