using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMonster : Monster
{
    protected override void StateMachineInit()
    {
        stateMachine = new StateMachine();
        stateMachine.stateDic.Add(EState.Walk, new Monster_Walk(this));
        stateMachine.CurState = stateMachine.stateDic[EState.Walk];
    }
}
