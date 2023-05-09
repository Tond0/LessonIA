using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : AIState
{
    float FlightDistance = 1;
    Vector3 inversedDirection;
    public Flight(GameObject npc, UnityEngine.AI.NavMeshAgent agent, Animator anim, Transform player, Transform[] checkpoints)
        : base(npc, agent, anim, player, checkpoints)
    {
        name = State.Flight;
    }

    public override void Enter()
    {
        //TODO: Animazione dove scappa come un matto impazzito
        inversedDirection = npc.transform.position - player.position * FlightDistance;
        agent.SetDestination(inversedDirection);

        base.Enter();
    }

    public override void Update()
    {
        if(agent.remainingDistance < 2)
        {
            nextState = new Idle(npc, agent, anim, player, checkpoints);
            stage = Event.Exit;
            return;
        }

        Debug.DrawRay(npc.transform.position, inversedDirection, Color.yellow);

        base.Update();
    }

    public override void Exit()
    {
        //TODO: Smette di scappare come un matto impazzito
        base.Exit();
    }

}
