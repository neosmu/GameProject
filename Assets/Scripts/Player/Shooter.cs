using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private BubblePool bubblePool;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private AudioClip bubbleShootSFX;

    public void Fire(bool isFlipped)
    {
        Vector2 direction = isFlipped ? Vector2.right : Vector2.left;
        PooledBubble bubble = bubblePool.GetBubble();
        if (bubble == null) return;

        bubble.transform.position = muzzleTransform.position;
        bubble.gameObject.SetActive(true);
        bubble.shoot(direction);
        AudioManager audio = FindObjectOfType<AudioManager>();
        if (audio != null && bubbleShootSFX != null)
        {
            audio.PlaySFX(bubbleShootSFX);
        }
    }
    
}
