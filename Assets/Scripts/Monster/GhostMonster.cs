using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monster_Fly;

public class GhostMonster : Monster
{
    protected override void StateMachineInit()
    {
        patrolVec = Vector2.left;

        stateMachine = new StateMachine();
        stateMachine.stateDic.Add(EState.Fly, new Monster_Fly(this));
        stateMachine.stateDic.Add(EState.Idleing, new Monster_Idle(this));
        stateMachine.CurState = stateMachine.stateDic[EState.Fly];
    }
}
