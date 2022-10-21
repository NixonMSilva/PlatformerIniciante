using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _playerRef;

    [SerializeField] private float _maxAttackDistance = 7f;

    [SerializeField] private float _cooldown = 0.5f;
    private float _cooldownElapsed = 0f;
    private bool _canShoot = true;

    private void Update ()
    {
        if (_canShoot && _playerRef != null
            && (Vector2.Distance(transform.position, _playerRef.position) <= _maxAttackDistance))
        {
            Shoot();
        }

        // Cooldown Processing
        if (!_canShoot)
        {
            _cooldownElapsed += Time.deltaTime;
            if (_cooldownElapsed >= _cooldown)
            {
                _cooldownElapsed = 0f;
                _canShoot = true;
            }
        }
    }

    private void Shoot ()
    {
        _canShoot = false;

        Vector2 playerDir = _playerRef.position - transform.position;

        GameObject newBullet = Instantiate(_bullet, transform.position, Quaternion.identity);

        if (newBullet != null)
        {
            Bullet normal;
            HomingBullet homing;

            newBullet.TryGetComponent<Bullet>(out normal);
            newBullet.TryGetComponent<HomingBullet>(out homing);

            if (normal)
                normal.SetTranslation(playerDir);

            if (homing)
                homing.SetPlayerRef(_playerRef);

        }
    }

}
