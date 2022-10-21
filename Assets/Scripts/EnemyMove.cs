using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    private void Start ()
    {
        Move();
        ChangeColor();
    }

    private void Move ()
    {
        transform.DOMoveX(transform.position.x + 5f, 2f, false).OnComplete(() =>
        transform.DOMoveX(transform.position.x - 5f, 2f, false).OnComplete(Move));
    }

    private void ChangeColor ()
    {
        GetComponent<SpriteRenderer>().DOColor(Color.cyan, 2f);
    }
}
