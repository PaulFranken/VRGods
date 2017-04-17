using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BuildAction : Action {

    private Follower followerScript;
    private GameObject storagePoint;
    private GameObject building;
    private Building blueprints;
    private GameManager gameManager;
    private bool hasResource = false;
    private bool isFinished = false;

    public BuildAction(GameObject follower, GameObject building)
    {
        this.follower = follower;
        this.followerScript = follower.GetComponent<Follower>();
        this.targetGameObject = building;
        this.storagePoint = GameObject.FindGameObjectWithTag("Storage");
        this.building = building;
        this.blueprints = building.GetComponent<Building>();
        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.blueprints.assignees.Add(follower);
        Debug.Log(blueprints.requirementsList[0].type);
        StartAction(targetGameObject);
    }

    public override void StartAction(GameObject g)
    {
        this.followerAgent = follower.GetComponent<NavMeshAgent>();
        this.followerAgent.SetDestination(storagePoint.transform.position);
        this.followerAgent.stoppingDistance = 0f;
    }

    public override void StartAction(Vector3 t)
    {
    }

    public override void PerformAction()
    {
        if(followerScript.currentCollidingObject != null && followerScript.currentCollidingObject == storagePoint && !hasResource)
        {
            if (followerScript.resource != null && followerScript.currentLoad > 0)
            {
                gameManager.AddResource(followerScript.resource.resourceType, followerScript.itemAmount);
                followerScript.currentLoad = 0;
                followerScript.itemAmount = 0;
                if (isFinished)
                {
                    followerScript.RemoveAction();

                }
            }
            foreach (Building.Requirements r in blueprints.requirementsList)
            {
                if (r.currentAmount < r.requiredAmount)
                {
                    followerScript.resource = ResourceTypes.GetByType(r.type);
                    while (followerScript.currentLoad <= followerScript.storageCapacity - followerScript.resource.resourceWeight)
                    {
                        followerScript.itemAmount++;
                        followerScript.currentLoad += followerScript.resource.resourceWeight;
                        gameManager.RemoveResource(followerScript.resource.resourceType, 1);
                    }
                    followerAgent.SetDestination(building.transform.position);
                    hasResource = true;
                    break;
                }
            }
        }
        if (followerScript.currentCollidingObject != null && followerScript.currentCollidingObject == building && hasResource)
        {
            if (isFinished)
            {
                followerAgent.SetDestination(storagePoint.transform.position);
            }
            else
            {
                if (followerScript.itemAmount > 0)
                {
                    foreach (Building.Requirements r in blueprints.requirementsList)
                    {
                        if (r.currentAmount < r.requiredAmount)
                        {
                            while (r.currentAmount < r.requiredAmount)
                            {
                                r.currentAmount++;
                                followerScript.itemAmount--;
                                followerScript.currentLoad -= followerScript.resource.resourceWeight;
                            }
                            followerAgent.SetDestination(storagePoint.transform.position);
                            break;
                        }

                    }
                    hasResource = false;


                }
                else
                {
                    followerAgent.SetDestination(storagePoint.transform.position);
                }
            }
            
        }
    }

    public override void EndAction()
    {
        isFinished = true;
    }

    
    
}

