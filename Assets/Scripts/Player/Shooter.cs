using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private BubblePool bubblePool;
    [SerializeField] private Transform muzzleTransform;

    public void Fire(bool isFacingRight)
    {
        Vector2 fireDir = isFacingRight ? Vector2.right : Vector2.left;

        PooledBubble bubble = bubblePool.GetBubble();
        bubble.transform.position = muzzleTransform.position;
        bubble.gameObject.SetActive(true);
        bubble.shoot(fireDir);
    }
}
