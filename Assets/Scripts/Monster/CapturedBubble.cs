using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturedBubble : MonoBehaviour
{
    [SerializeField] private AudioClip bubblePopSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager audio = FindObjectOfType<AudioManager>();
            if (audio != null && bubblePopSFX != null)
            {
                audio.PlaySFX(bubblePopSFX);
            }
            PlayerModel model = collision.GetComponent<PlayerModel>();
            if (model != null)
            {
                model.Score += 500;
            }
            Destroy(gameObject);
        }
    }
}
