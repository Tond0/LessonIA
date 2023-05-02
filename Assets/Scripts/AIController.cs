using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform[] checkpoints;
    
    NavMeshAgent agent;
    Animator anim;
    AIState currentState;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //anim = transform.GetChild(0).GetComponent<Animator>();
        currentState = new Idle(gameObject, agent, anim, player, checkpoints);
    }

    private void Update()
    {
        currentState = currentState.Process();
        Debug.Log(currentState);
    }
}
