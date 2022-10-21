using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _playerRef;

    private Vector2 _toPlayer;
    private float _angleToPlayer;

    [SerializeField] private float _turretSpread = 15f;

    [SerializeField] private float _minAttackDistance = 7f;

    [SerializeField] private float _cooldown = 0.5f;
    private float _cooldownElapsed = 0f;
    private bool _canShoot = true;

    private void Update()
    {
        _toPlayer = _playerRef.position - transform.position;
        _angleToPlayer = Vector2.SignedAngle(Vector2.up, _toPlayer.normalized);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, _angleToPlayer));

        if (_canShoot && _playerRef != null
            && (Vector2.Distance(transform.position, _playerRef.position) <= _minAttackDistance))
            Shoot();

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

    private void Shoot()
    {
        _canShoot = false;

        Vector2 toPlayerSpread = Quaternion.AngleAxis(Random.Range(-_turretSpread, _turretSpread), Vector3.forward) *
            _toPlayer;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, toPlayerSpread.normalized);

        if (hit && hit.rigidbody.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player!");
            Debug.DrawRay(transform.position, toPlayerSpread, Color.green, 2f);
        }
        else
        {
            Debug.DrawRay(transform.position, toPlayerSpread, Color.red, 2f);
        }
    }

    [ContextMenu("Locate Player")]
    private void LocatePlayer()
    {
        var playerTemp = GameObject.Find("Player");

        if (playerTemp != null)
            _playerRef = playerTemp.transform;
    }
}
