using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledBubble : MonoBehaviour
{
    private Rigidbody2D rigid;
    private BubblePool pool;

    [SerializeField] private float forwardForce;
    [SerializeField] private float upwardForce;
    [SerializeField] private float floatDelay;
    [SerializeField] private GameObject capturedBubbleMonsterPrefab;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            rigid.velocity = Vector2.zero;
            rigid.bodyType = RigidbodyType2D.Kinematic;

            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.ChangeState(new Monster_Captured(monster, this.gameObject, capturedBubbleMonsterPrefab));
            }
        }

    }
}
