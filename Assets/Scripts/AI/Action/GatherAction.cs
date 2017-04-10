using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GatherAction : Action {

    Follower followerScript;
    GameObject storagePoint;
    private int resourceWeight;
    private bool isFull = false;
    private bool isCooldown = false;
    private float cooldownTimer = 0f;

    public GatherAction(GameObject follower, GameObject g)
    {
        this.follower = follower;
        StartAction(g);
    }

    public override void StartAction(GameObject g)
    {
        this.targetGameObject = g;
        this.followerAgent = follower.GetComponent<NavMeshAgent>();
        this.followerAgent.SetDestination(this.targetPosition);
        this.followerScript = follower.GetComponent<Follower>();
        this.storagePoint = GameObject.FindGameObjectWithTag("Storage");
        this.resourceWeight = g.GetComponent<Resource>().resourceWeight;
        this.followerAgent.SetDestination(targetGameObject.transform.position);
        Debug.Log("ZAWARUDO");
    }

    public override void StartAction(Vector3 t)
    {
    }

    public override void PerformAction()
    {

        if(followerScript.currentCollidingObject == targetGameObject)
        {
            if (!isFull && !isCooldown && followerScript.currentLoad < followerScript.storageCapacity - resourceWeight)
            {
                this.followerScript.itemAmount++;
                this.followerScript.currentLoad += resourceWeight;
                isCooldown = true;
            }
            else if(!(followerScript.currentLoad < followerScript.storageCapacity - resourceWeight))
            {
                this.isFull = true;
            }
            if (isFull)
            {
                this.followerAgent.SetDestination(storagePoint.transform.position);
            }
        } else if(followerScript.currentCollidingObject == storagePoint)
        {
            this.followerScript.itemAmount = 0;
            this.followerScript.currentLoad = 0;
            this.isFull = false;
            this.followerAgent.SetDestination(targetGameObject.transform.position);
        }

        if (isCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer > 1f)
            {
                isCooldown = false;
            }
        }
        
    }

    public override void EndAction()
    {
        
    }

    

    

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
