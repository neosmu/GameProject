using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private BubblePool bubblePool;
    [SerializeField] private Transform muzzleTransform;

    public void Fire(bool isFlipped)
    {
        Vector2 direction = isFlipped ? Vector2.right : Vector2.left;
        PooledBubble bubble = bubblePool.GetBubble();
        if (bubble == null) return;

        bubble.transform.position = muzzleTransform.position;
        bubble.gameObject.SetActive(true);
        bubble.shoot(direction);
    }
    public void SetMuzzleDirection(bool isFacingRight)
    {
        Vector3 localPos = muzzleTransform.localPosition;
        localPos.x = Mathf.Abs(localPos.x) * (isFacingRight ? 1 : -1);
        muzzleTransform.localPosition = localPos;
    }
}
