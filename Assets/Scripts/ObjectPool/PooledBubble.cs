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
    [SerializeField] private AudioClip breakSound;
    private AudioSource audioSource;

    private bool isCaptured = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        isCaptured = false;
        StartCoroutine(AutoBreak());
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
    private IEnumerator AutoBreak()
    {
        yield return new WaitForSeconds(3f);

        if (!isCaptured)
        {
            BreakBubble();
        }
    }
    private void BreakBubble()
    {
        if (audioSource != null && breakSound != null)
        {
            audioSource.PlayOneShot(breakSound);
        }
        Invoke(nameof(Return), 0.1f);
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            rigid.velocity = Vector2.zero;
            rigid.bodyType = RigidbodyType2D.Kinematic;
            isCaptured = true;

            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.ChangeState(new Monster_Captured(monster, this.gameObject, capturedBubbleMonsterPrefab));
            }
        }
    }
}
