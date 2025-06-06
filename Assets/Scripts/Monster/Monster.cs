using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] public float moveSpeed;

    public StateMachine stateMachine;
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Vector2 patrolVec = Vector2.left;

    public readonly int IDLE_HASH = Animator.StringToHash("Idle");
    public readonly int WALK_HASH = Animator.StringToHash("Walk");
    public readonly int FLY_HASH = Animator.StringToHash("Fly");
    public readonly int IDLEING_HASH = Animator.StringToHash("Idleing");

    protected virtual void Awake()
    {
        StateMachineInit();
    }
    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected abstract void StateMachineInit();

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    public void ChangeState(BaseState newState)
    {
        stateMachine.ChangeState(newState);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            patrolVec *= -1f;
        }
        if (collision.gameObject.CompareTag("Monster"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
    }
}
