using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    [SerializeField] private GameObject _endGoalText;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Fim de jogo
            _endGoalText.SetActive(true);

            // Desativando controles do player
            Destroy(collision.gameObject.GetComponent<MovementPlatformer>());
        }
    }
}
