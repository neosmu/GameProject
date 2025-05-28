using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{

    [SerializeField] private float moveSpeed; // 움직이는 스피드
    [SerializeField] private float jumpSpeed;

    private Rigidbody2D rigid;
    private Vector2 inputVec;
    private float inputX;
    private bool isJumped;
    private bool isGrounded;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumped = true;
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
        rigid.velocity = new Vector2(inputX * moveSpeed, rigid.velocity.y);
    }

    private void PlayerJump()
    {
        rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        isJumped = false;
        isGrounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
