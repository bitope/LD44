using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class m1_test : MonoBehaviour
{
    public List<Transform> waypoints;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Vector3.zero);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 251 == 0)
        {
            agent.SetDestination(waypoints[Time.frameCount%waypoints.Count].position);
        }
    }
}
