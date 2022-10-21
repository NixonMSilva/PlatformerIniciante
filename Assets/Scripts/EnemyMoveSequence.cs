using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMoveSequence : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private Sequence _moveSequence;

    private void Start ()
    {
        if (_waypoints.Length < 1)
        {
            Debug.LogWarning("No waypoint set for (" + gameObject.name + ")!");
            return;
        }

        _moveSequence = DOTween.Sequence().SetAutoKill(false);
        _moveSequence.SetLoops(-1, LoopType.Yoyo);

        foreach (Transform point in _waypoints)
        {
            float timeToMove = CalcTime(Vector2.Distance(transform.position, point.position));
            _moveSequence.Append(transform.DOMove(point.position, timeToMove).SetEase(Ease.OutQuad));
        }
    }

    private float CalcTime (float distance)
    {
        return (distance / _speed);
    }
}
