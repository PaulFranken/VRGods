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

    private bool hasAction = false;
    private bool isWalking = false;
    private bool isGathering = false;
    private bool targetSet = false;

    private GameObject resourceTarget;

    public Vector3 target;
    public NavMeshAgent agent;
    public Action action;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        
    }

    // Update is called once per frame
    void Update () {
        if (hasAction)
        {
            action.PerformAction();
        }
    }

    public void SetAction(string actionType, Vector3 target, GameObject targetGameObject)
    {
        if (!targetGameObject)
        {
            this.action = new MoveAction(this.gameObject, target);
            this.hasAction = true;
        }
    }

    public void RemoveAction()
    {

        this.action = null;
        this.hasAction = false;
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
