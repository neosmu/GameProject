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
