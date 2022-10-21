using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed;

    private Transform _player;
    private bool _canChase = false;

    private void FixedUpdate()
    {
        if (!_canChase) return;

        Vector2 nextDir = _player.position - transform.position;
        transform.position = ((Vector2)transform.position)
            + (nextDir.normalized * _chaseSpeed * Time.fixedDeltaTime);
    }

    public void SetPlayerRef (Transform player)
    {
        _player = player;
        _canChase = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
