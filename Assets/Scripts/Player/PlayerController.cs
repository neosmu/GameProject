using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerModel model;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Shooter shooter;

    private Rigidbody2D rigid;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDeath = false;

    private float inputX;
    private bool isJumped;
    private bool isGrounded;

    private readonly int IDLE_HASH = Animator.StringToHash("Idle");
    private readonly int MOVE_HASH = Animator.StringToHash("Move");
    private readonly int JUMP_HASH = Animator.StringToHash("Jump");
    private readonly int HIT_HASH = Animator.StringToHash("Hit");
    private readonly int DEATH_HASH = Animator.StringToHash("Death");

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDeath) return;
        PlayerInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumped = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            shooter.Fire(spriteRenderer.flipX);
        }
    }

    private void FixedUpdate()
    {
        if (isDeath) return;
        PlayerMove();
        if (isJumped && isGrounded)
            PlayerJump();
    }

    private void PlayerInput()
    {
        inputX = Input.GetAxis("Horizontal");
    }

    private void PlayerMove()
    {
        if (inputX == 0)
        {
            animator.Play(IDLE_HASH);
            return;
        }
        rigid.velocity = new Vector2(inputX * moveSpeed, rigid.velocity.y);
        animator.Play(MOVE_HASH);
        spriteRenderer.flipX = inputX > 0;
    }

    private void PlayerJump()
    {
        rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        animator.Play(JUMP_HASH);
        isJumped = false;
        isGrounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Monster") && !isDeath)
        {
            if (model != null)
            {
                model.HP -= 1;
                if (model.HP <= 0)
                {
                    animator.Play(DEATH_HASH);
                    rigid.velocity = Vector2.zero;
                    isDeath = true;
                }
                else
                {
                    animator.Play(HIT_HASH);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Catch"))
        {
            Destroy(other.gameObject);
        }
    }
}
