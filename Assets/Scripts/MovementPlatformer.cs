using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatformer : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpStrength;

    [SerializeField] private float _wallJumpMovementCooldown = 0.5f;
    private float _wallJumpMovementCooldownElapsed = 0f;
    private bool _wallJumpPerformed = false;

    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private Collider2D[] _sides;

    private float _movement = 0f;

    private bool _isGrounded = true;

    private void Awake ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update ()
    {
        _movement = Input.GetAxisRaw("Horizontal");

        if (_movement != 0f)
            Move();

        _isGrounded = CheckGroundedStatus();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
                Jump();

            int side = CheckSidewaysToGround();

            if (side != 0)
            {
                WallJump(side * -1f);
            }
        }

        if (_wallJumpPerformed)
        {
            _wallJumpMovementCooldownElapsed += Time.deltaTime;
            if (_wallJumpMovementCooldownElapsed >= _wallJumpMovementCooldown)
            {
                _wallJumpPerformed = false;
            }
        }
    }

    private void WallJump (float side)
    {
        _rigidBody.AddForce(new Vector2(side, 0.4f) * _jumpStrength * 0.4f, ForceMode2D.Impulse);
        _wallJumpPerformed = true;
        _wallJumpMovementCooldownElapsed = 0f;
    }

    private bool CheckGroundedStatus ()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,
            0f, Vector2.down, 0.1f, _groundMask);

        return (raycastHit.collider != null);
    }

    private int CheckSidewaysToGround ()
    {
        if (_sides[0].IsTouchingLayers(_groundMask)) return 1;
        if (_sides[1].IsTouchingLayers(_groundMask)) return -1;
        return 0;
    }

    private void Move ()
    {
        //if (CheckSidewaysToGround() != 0) return;

        //if (_wallJumpPerformed) return;

        _rigidBody.velocity = new Vector2(_movement * _speed, _rigidBody.velocity.y);

        Debug.Log("Y: " + _rigidBody.velocity.y);
    }

    private void Jump ()
    {
        // Modo alternativo de fazer o pulo
        //_rigidBody.velocity += new Vector2(0f, _jumpStrength);

        _rigidBody.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected ()
    {
        if (_boxCollider == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + (Vector3)(Vector2.down * 0.1f), _boxCollider.bounds.size);
    }
}
