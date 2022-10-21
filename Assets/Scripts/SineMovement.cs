using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 4f;
    [SerializeField] private float _arcLenght = 0.2f;
    [SerializeField] private float _arcAmplitude = 2f;

    private void FixedUpdate ()
    {
        // (1, 0)
        float posX = transform.position.x + (Vector2.right.x * _horizontalSpeed * Time.fixedDeltaTime);
        float posY = _arcAmplitude * Mathf.Sin(Time.time * (1f / _arcLenght));
        transform.position = new Vector2(posX, posY);
    }
}
