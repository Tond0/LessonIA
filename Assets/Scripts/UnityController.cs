using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class UnityController : MonoBehaviour
{
    List<NavMeshAgent> agents = new List<NavMeshAgent>();
    [SerializeField] LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        agents.AddRange(FindObjectsOfType<NavMeshAgent>());
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, groundMask))
        {
            foreach (NavMeshAgent agent in agents)
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
