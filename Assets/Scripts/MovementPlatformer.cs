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

    [SerializeField] private LayerMask _groundMask;

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

        _isGrounded = CheckGroundedStatus();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();
    }

    private void FixedUpdate ()
    {
        if (_movement != 0f)
            Move();
    }

    private bool CheckGroundedStatus ()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,
            0f, Vector2.down, 0.1f, _groundMask);

        return (raycastHit.collider != null);
    }

    private void Move ()
    {
        _rigidBody.velocity = new Vector2(_movement * _speed, _rigidBody.velocity.y);
    }

    private void Jump ()
    {
        _rigidBody.velocity += new Vector2(0f, _jumpStrength);
    }

    private void OnDrawGizmosSelected ()
    {
        if (_boxCollider == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + (Vector3)(Vector2.down * 0.1f), _boxCollider.bounds.size);
    }
}
