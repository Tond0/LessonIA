using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Patrol : AIState
{
    int currentCheckPoint = -1;

    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] checkpoints)
        : base(npc, agent, anim, player, checkpoints)
    {
        name = State.Patrol;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        currentCheckPoint = 0;
        agent.SetDestination(checkpoints[currentCheckPoint].position);
        //TODO: Set animazione di camminata

        base.Enter();
    }

    public override void Update()
    {
        if(agent.remainingDistance < agent.stoppingDistance)
        {
            if (currentCheckPoint >= checkpoints.Length - 1)
                currentCheckPoint = 0;
            else
                currentCheckPoint++;

            agent.SetDestination(checkpoints[currentCheckPoint].position);
        }
        base.Update();
    }

    public override void Exit()
    {
        //TODO: Reset animazione camminata
        base.Exit();
    }
}
