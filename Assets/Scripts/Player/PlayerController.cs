using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] PlayerModel model;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Shooter shooter;

    private Rigidbody2D rigid;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float inputX;
    private bool isJumped;
    private bool isGrounded;

    private readonly int IDLE_HASH = Animator.StringToHash("Idle");
    private readonly int MOVE_HASH = Animator.StringToHash("Move");
    private readonly int JUMP_HASH = Animator.StringToHash("Jump");

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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
        if (collision.collider.CompareTag("Monster"))
        {
            if (model != null)
            {
                model.HP -= 1;
            }
        }
    }
}
