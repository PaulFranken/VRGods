using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Resources;
using UnityEngine;
using UnityEngine.AI;

public class GatherAction : Action {

    Follower followerScript;
    GameObject storagePoint;
    private Game Game;
    private int resourceWeight;
    private bool isFull = false;
    private bool isCooldown = false;
    private bool lastCall = false;
    private float cooldownTimer = 0f;

    public GatherAction(GameObject follower, GameObject g)
    {
        this.follower = follower;
        Game = GameObject.Find("Game").GetComponent<Game>();
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
        this.followerAgent.stoppingDistance = 0f;

    }

    public override void StartAction(Vector3 t)
    {
    }

    public override void PerformAction()
    {

        if(followerScript.currentCollidingObject == targetGameObject)
        {
            if (!isFull && !isCooldown && followerScript.currentLoad < followerScript.storageCapacity - resourceWeight && targetGameObject.GetComponent<Resource>().resourceAmount > 0 && lastCall == false)
            {
                this.followerScript.itemAmount++;
                this.followerScript.currentLoad += resourceWeight;
                this.targetGameObject.GetComponent<Resource>().resourceAmount--;
                isCooldown = true;
            }
            else if(!(followerScript.currentLoad < followerScript.storageCapacity - resourceWeight))
            {
                this.isFull = true;
            }
            else if(targetGameObject.GetComponent<Resource>().resourceAmount <= 0)
            {
                lastCall = true;
                this.followerAgent.SetDestination(storagePoint.transform.position);
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
            if (!lastCall)
            {
                this.followerAgent.SetDestination(targetGameObject.transform.position);
            }
            else
            {
                EndAction();
            }
        }

        if (isCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer > 1f)
            {
                isCooldown = false;
                cooldownTimer = 0f;
            }
        }
        
    }

    public override void EndAction()
    {
        followerScript.RemoveAction();
    }

    

    

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
