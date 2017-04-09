using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour {

    public float health;
    public float speed;
    public float storageCapacity;
    public string followerName;
    public int strength;
    public int intellect;

    private bool isWalking = false;
    private bool isGathering = false;

    private GameObject resourceTarget;

    public Vector3 target;
    private NavMeshAgent agent;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        
    }

    // Update is called once per frame
    void Update () {

        if (resourceTarget != null)
        {
            agent.SetDestination(resourceTarget.transform.position);
        }
        else if (target != null)
        {
            agent.SetDestination(target);
        }
        
    }

    public void SetTarget(Vector3 t)
    {
        target = t;
    }

    public void GatherResource(GameObject g)
    {
        
        target = g.transform.position;
        Debug.Log(target);
        isGathering = true;
        resourceTarget = g;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(resourceTarget.name);
        if (other.gameObject == resourceTarget)
        {
            Debug.Log("TRIGGERED");
        }
    }
}
