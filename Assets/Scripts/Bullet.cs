using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _translationSpeed;

    private Vector2 _translationDir;
    private bool _canTranslate = false;

    private void FixedUpdate ()
    {
        if (!_canTranslate) return;

        transform.position = ((Vector2)transform.position)
            + (_translationDir.normalized * _translationSpeed * Time.fixedDeltaTime);
    }

    public void SetTranslation (Vector2 direction)
    {
        _translationDir = direction;
        _canTranslate = true;
    }

    private void OnTriggerEnter2D (Collider2D collision)
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
