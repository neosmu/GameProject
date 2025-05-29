using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMonster : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rigid;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float direction = -1f;
    private readonly int IDLE_HASH = Animator.StringToHash("Idle");
    private readonly int WALK_HASH = Animator.StringToHash("Walk");

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        rigid.velocity = new Vector2(direction * moveSpeed, rigid.velocity.y);

        animator.Play(WALK_HASH);
        spriteRenderer.flipX = direction > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            direction *= -1f;
        }
    }
}
