using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BuildAction : Action {

    Follower followerScript;
    GameObject storagePoint;
    ResourceMap.ResourceEntry[] resources;
    
    


    public BuildAction(GameObject follower, GameObject building)
    {
        this.follower = follower;
        this.targetGameObject = building;
        this.storagePoint = GameObject.FindGameObjectWithTag("Storage");
        this.resources = targetGameObject.GetComponent<ResourceMap>().resources;
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
        if(followerScript.currentCollidingObject == storagePoint)
        {
            if (followerScript.currentLoad == 0)
            {
                foreach (ResourceMap.ResourceEntry r in resources)
                {
                    if(r.current < r.amount)
                    {
                        followerScript.resource = r.resource;
                        //followerScript.currentLoad =
                    }
                }    
            }
        }
    }

    public override void EndAction()
    {
    }

    
    
}

