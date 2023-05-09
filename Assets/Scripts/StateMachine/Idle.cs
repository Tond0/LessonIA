using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Idle : AIState
{
    public Idle(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] checkpoints) 
        : base(npc, agent, anim, player, checkpoints)
    {
        name = State.Idle;
    }

    public override void Enter()
    {
        //TODO: Set animazioni idle
        //Richiamiamo quello fatto in AIState 
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Chase(npc, agent, anim, player, checkpoints);
            stage = Event.Exit;
            return;
        }
        else if (PlayerBehind())
        {
            nextState = new Flight(npc, agent, anim, player, checkpoints);
            stage = Event.Exit;
            return;
        }
        //2% di chanche
        if(Random.Range(0, 500) < 10)
        {
            nextState = new Patrol(npc, agent, anim, player, checkpoints);
            stage = Event.Exit;
            return;
        }

    }

    public override void Exit()
    {
        //TODO: reset animazione.
        base.Exit();
    }

}
