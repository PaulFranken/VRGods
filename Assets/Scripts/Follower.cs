using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour {

    public float health;
    public float speed;
    public Vector3 target;
    private NavMeshAgent agent;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update () {
        if (target != null)
        {
            agent.SetDestination(target);
        }
    }

    public void SetTarget(Vector3 t)
    {
        target = t;
    }
}
