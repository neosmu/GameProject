using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterState : BaseState
{
    protected Monster monster;

    public MonsterState(Monster _monster)
    {
        monster = _monster;
    }

    public override void Enter() { }
    public override void Update() { }
    public override void Exit() { }
}

public class Monster_Walk : MonsterState
{
    public Monster_Walk(Monster _monster) : base(_monster)
    {
        HasPhysics = true;
    }

    public override void Enter()
    {
        monster.animator.Play(monster.WALK_HASH);
    }

    public override void FixedUpdate()
    {
        monster.spriteRenderer.flipX = monster.patrolVec.x > 0;
        monster.rigid.velocity = new Vector2(monster.patrolVec.x * monster.moveSpeed, monster.rigid.velocity.y);
    }
}

public class Monster_Fly : MonsterState
{
    private float timer = 0f;
    private float switchTime = 3f;

    public Monster_Fly(Monster monster) : base(monster)
    {
        HasPhysics = true;
    }

    public override void Enter()
    {
        timer = 0f;
        monster.animator.Play(monster.FLY_HASH);
    }

    public override void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        Vector2 velocity = monster.patrolVec.normalized * monster.moveSpeed;
        monster.rigid.velocity = velocity;

        if (monster.patrolVec.x != 0)
            monster.spriteRenderer.flipX = monster.patrolVec.x > 0;

        if (timer >= switchTime)
        {
            monster.ChangeState(monster.stateMachine.stateDic[EState.Idleing]);
        }
    }
    public class Monster_Idle : MonsterState
    {
        private float timer = 0f;
        private float switchTime = 3f;

        public Monster_Idle(Monster monster) : base(monster)
        {
            HasPhysics = false;
        }

        public override void Enter()
        {
            timer = 0f;
            monster.rigid.velocity = Vector2.zero;
            monster.animator.Play(monster.IDLEING_HASH);
        }

        public override void Update()
        {
            timer += Time.deltaTime;

            if (timer >= switchTime)
            {
                Vector2[] dirs = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
                monster.patrolVec = dirs[Random.Range(0, dirs.Length)];

                monster.ChangeState(monster.stateMachine.stateDic[EState.Fly]);
            }
        }
    }
}
public class Monster_Captured : MonsterState
{
    private GameObject bubbleObj; 
    private GameObject prefab;       

    public Monster_Captured(Monster _monster, GameObject bubble, GameObject capturedPrefab) : base(_monster)
    {
        HasPhysics = false;
        this.bubbleObj = bubble;
        this.prefab = capturedPrefab;
    }
    public override void Enter()
    {
        monster.gameObject.SetActive(false);
        bubbleObj.SetActive(false);
        Object.Instantiate(prefab, monster.transform.position, Quaternion.identity);
    }
}
