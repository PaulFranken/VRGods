using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Resources;
using UnityEngine;
using UnityEngine.AI;

public class GatherAction : Action
{

    Follower followerScript;
    GameObject storagePoint;
    private Resource targetResource;
    private GameManager gameManager;
    private int resourceWeight;
    private bool isFull = false;
    private bool isCooldown = false;
    private bool lastCall = false;
    private bool hasPreviousResource = false;
    private float cooldownTimer = 0f;


    public GatherAction(GameObject follower, GameObject g)
    {
        this.follower = follower;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartAction(g);
    }

    public override void StartAction(GameObject g)
    {
        this.targetGameObject = g;
        this.targetResource = g.GetComponent<Resource>();
        this.followerAgent = follower.GetComponent<NavMeshAgent>();
        this.followerAgent.SetDestination(this.targetPosition);
        this.followerScript = follower.GetComponent<Follower>();
        this.storagePoint = GameObject.FindGameObjectWithTag("Storage");
        this.resourceWeight = g.GetComponent<Resource>().resourceWeight;
        this.followerAgent.SetDestination(targetGameObject.transform.position);
        this.followerAgent.stoppingDistance = 0f;
        if (followerScript.resource.GetType() != targetResource.GetType() && followerScript.currentLoad > 0)
        {
            hasPreviousResource = true;
        }
        else
        {
            followerScript.resource = ResourceTypes.GetByType(targetResource.resourceType);
        }

    }

    public override void StartAction(Vector3 t)
    {
    }

    public override void PerformAction()
    {
        //Check if follower still has leftover resource of a different type

        if (hasPreviousResource)
        {
            followerAgent.SetDestination(storagePoint.transform.position);
            if (followerScript.currentCollidingObject == storagePoint)
            {
                this.gameManager.AddResource(followerScript.resource.resourceType, followerScript.itemAmount);
                this.followerScript.itemAmount = 0;
                this.followerScript.currentLoad = 0;
                this.hasPreviousResource = false;
                followerScript.resource = ResourceTypes.GetByType(targetResource.resourceType);
            }
        }
        else
        {
            if (followerScript.currentCollidingObject == targetGameObject)
            {
                if (!isFull && !isCooldown &&
                    followerScript.currentLoad <= followerScript.storageCapacity - resourceWeight &&
                    targetGameObject.GetComponent<Resource>().resourceAmount > 0 && lastCall == false)
                {
                    this.followerScript.itemAmount++;
                    this.followerScript.currentLoad += resourceWeight;
                    this.targetGameObject.GetComponent<Resource>().resourceAmount--;
                    isCooldown = true;
                }
                else if (!(followerScript.currentLoad < followerScript.storageCapacity - resourceWeight))
                {
                    this.isFull = true;
                }
                else if (targetGameObject.GetComponent<Resource>().resourceAmount <= 0)
                {
                    lastCall = true;
                    this.followerAgent.SetDestination(storagePoint.transform.position);
                }
                if (isFull)
                {
                    this.followerAgent.SetDestination(storagePoint.transform.position);
                }
            }
            else if (followerScript.currentCollidingObject == storagePoint)
            {
                this.gameManager.AddResource(followerScript.resource.resourceType, followerScript.itemAmount);
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
                if (cooldownTimer > 1f)
                {
                    isCooldown = false;
                    cooldownTimer = 0f;
                }
            }
        }


    }

    public override void EndAction()
    {
        followerScript.RemoveAction();
    }
}
