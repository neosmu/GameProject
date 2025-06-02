using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturedBubble : MonoBehaviour
{
    [SerializeField] private AudioClip bubblePopSFX;
    [SerializeField] private GameObject[] fruitPrefabs;

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
                model.Score += 300;
            }
            if (fruitPrefabs != null && fruitPrefabs.Length > 0)
            {
                int index = Random.Range(0, fruitPrefabs.Length);
                Instantiate(fruitPrefabs[index], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
