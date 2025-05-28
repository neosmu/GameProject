using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] PlayerModel model;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private GameObject bubblePrefab;  
    [SerializeField] private Transform muzzleTransform; 

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
            Fire();
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
    private void Fire()
    {
        GameObject bubble = Instantiate(bubblePrefab, muzzleTransform.position, Quaternion.identity);

        // πÊ«‚ ∞·¡§ (flipX = øﬁ¬  πŸ∂Û∫Ω)
        float direction = spriteRenderer.flipX ? -1f : 1f;

        Rigidbody2D bubbleRb = bubble.GetComponent<Rigidbody2D>();
        if (bubbleRb != null)
        {
            bubbleRb.AddForce(Vector2.right * direction * 3f, ForceMode2D.Impulse);
            StartCoroutine(ApplyUpwardForceAfterDelay(bubbleRb, 0.2f));
        }
    }
    private IEnumerator ApplyUpwardForceAfterDelay(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
