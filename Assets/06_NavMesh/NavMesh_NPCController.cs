using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh_NPCController : MonoBehaviour
{
    public Transform npcDestination;
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = npcDestination.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
