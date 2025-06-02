using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItem : MonoBehaviour
{
    [SerializeField] private int scoreValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerModel model = collision.GetComponent<PlayerModel>();
            if (model != null)
            {
                model.Score += scoreValue;
            }
            Destroy(gameObject);
        }
    }
}
