using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Attack : AIState
{

    float rotationSpeed = 10.0f;

    AudioSource attackSound;

    public Attack(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] checkpoints)
        : base(npc, agent, anim, player, checkpoints)
    {
        name = State.Attack;
        attackSound = npc.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        agent.isStopped = true;
        if (attackSound)
            attackSound.Play();

        base.Enter();
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        if (!CanAttackPlayer())
        {
            nextState = new Idle(npc, agent, anim, player, checkpoints);
            stage = Event.Exit;
            return;

        }

        base.Update();
    }

    public override void Exit()
    {
        //TODO: Reset animazione d'attacco
        if (attackSound)
        {
            attackSound.Stop();
        }

        base.Exit();
    }
}
