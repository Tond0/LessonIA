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
        float lastDist = Mathf.Infinity;
        for(int i = 0; i < checkpoints.Length; i++)
        {
            Transform tempCheckpoint = checkpoints[i];
            float distance = Vector3.Distance(npc.transform.position, tempCheckpoint.position);
            if(distance < lastDist)
            {
                currentCheckPoint = i - 1;
                lastDist = distance;
            }
        }
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

        base.Update();
    }

    public override void Exit()
    {
        //TODO: Reset animazione camminata
        base.Exit();
    }
}
