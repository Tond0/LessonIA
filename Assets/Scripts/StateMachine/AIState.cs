using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//Da mettere dentro alla classe siccome solo le classi AIState lo utilizzano, ma ora da errore
public enum State
{
    Idle,
    Patrol,
    Alert,
    Chase,
    Flight,
    Attack,
    Death
}


public class AIState
{
    public enum Event
    {
        Enter,
        Update,
        Exit
    }

    public State name;

    //Protected perché nessuno se non se stesso deve gestire gli stati in cui si trova.
    protected Event stage;

    //Refs
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected Transform[] checkpoints;
    protected AIState nextState;
    protected NavMeshAgent agent;

    //Stats
    float visDist = 10.0f;
    float halfVisAngle = 30.0f;
    float halfBackStab = 30.0f;
    float attackRange = 7.0f;


    #region Costruttore
    public AIState(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] checkpoints)
    {
        this.npc = npc;
        this.agent = agent;
        this.anim = anim;
        this.player = player;
        this.checkpoints = checkpoints;
        
        stage = Event.Enter;
    }
    #endregion

    //Di default subito settiamo il prossimo stato (l'update)
    public virtual void Enter() { stage = Event.Update; }

    //Si risetta e si cicla da solo
    public virtual void Update() { stage = Event.Update; }

    //Si resetta perché dopo non deve fare più niente
    public virtual void Exit() { stage = Event.Exit; }

    public AIState Process()
    {
        if (stage == Event.Enter) Enter();
        if (stage == Event.Update) Update();
        if (stage == Event.Exit) 
        {
            Exit();
            //Restituiamo il prossimo stato
            return nextState;
        };

        //Restituiamo lo statu attuale (un modo per dire "noi siamo in questo stato qua")
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direzione = player.position - npc.transform.position;
        float angle = Vector3.Angle(direzione, npc.transform.forward);

        if (direzione.magnitude < visDist && angle < halfVisAngle)
        {
            return true;
        }

        return false;
    }
    public bool PlayerBehind()
    {
        Vector3 direzione = npc.transform.position - player.position;
        float angle = Vector3.Angle(direzione, player.forward);

        if (direzione.magnitude < visDist && angle < halfBackStab)
        {
            return true;
        }

        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;

        if(direction.magnitude < attackRange)
        {
            return true;
        }

        return false;
    }

}
