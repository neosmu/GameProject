using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledBubble : MonoBehaviour
{
    private Rigidbody2D rigid;
    private BubblePool pool;

    [SerializeField] private float forwardForce = 3f;
    [SerializeField] private float upwardForce = 2f;
    [SerializeField] private float floatDelay = 0.2f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void SetPool(BubblePool bubblePool)
    {
        pool = bubblePool;
    }

    public void shoot(Vector2 direction)
    {
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0f;
        rigid.AddForce(direction * forwardForce, ForceMode2D.Impulse);
        StartCoroutine(ApplyUpwardForce());
    }

    private IEnumerator ApplyUpwardForce()
    {
        yield return new WaitForSeconds(floatDelay);
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
    }
    public void Return()
    {
        gameObject.SetActive(false);
        if (pool != null)
        {
            pool.ReturnBubble(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
