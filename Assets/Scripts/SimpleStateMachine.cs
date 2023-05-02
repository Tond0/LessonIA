using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class SimpleStateMachine : MonoBehaviour
{
    public State state = State.Idle;

    [SerializeField] Transform[] checkpoints;

    NavMeshAgent agent;

    //All'inizio non abbiamo nessun checkpoint assegnato, siamo in idle
    int actualCheckPoint = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (state == State.Death) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.Patrol;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            state = State.Patrol;
        }

        switch (state)
        {
            case State.Idle:
                break;
            case State.Patrol:


                agent.SetDestination(checkpoints[actualCheckPoint].position);
                if (Vector3.Distance(transform.position, checkpoints[actualCheckPoint].position) <= agent.stoppingDistance)
                    actualCheckPoint++;


                break;
            case State.Alert:
                break;
            case State.Chase:
                transform.Translate(Vector3.forward * 5 * Time.deltaTime);
                break;
            default:
                break;
        }

    }
}
