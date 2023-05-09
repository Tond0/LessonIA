using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Chase : AIState
{
    public Chase(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] checkpoints)
        : base(npc, agent, anim, player, checkpoints)
    {
        name = State.Chase;
        agent.speed = 6;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        //TODO: animazione corsa

        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        //Ci mette più di 1 frame per calcolare la
        //posizione quindi prima di fare il resto gli facciamo
        //calcolare il percorso dandogli tutti i frame di cui ha bisogno
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player, checkpoints);
                stage = Event.Exit;
                return;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, anim, player, checkpoints);
                stage = Event.Exit;
                return;
            }
        }

        base.Update();
    }

    public override void Exit()
    {
        //TODO: Stop run animation
        base.Exit();
    }
}
