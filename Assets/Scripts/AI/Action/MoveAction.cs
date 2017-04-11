using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAction : Action
{

    public MoveAction(GameObject follower, Vector3 target)
    {
        this.follower = follower;
        StartAction(target);
    }

    public override void StartAction(Vector3 t)
    {
        this.targetPosition = t;
        this.followerAgent = follower.GetComponent<NavMeshAgent>();
        this.followerAgent.SetDestination(this.targetPosition);
        this.followerAgent.stoppingDistance = 5f;
    }


    public override void StartAction(GameObject g)
    {
        
    }

    public override void PerformAction()
    {
        

        if (!followerAgent.pathPending)
        {
            if (followerAgent.remainingDistance <= followerAgent.stoppingDistance)
            {
                if (!followerAgent.hasPath || followerAgent.velocity.sqrMagnitude == 0f)
                {
                    EndAction();
                }
            }
        }
    }

    public override void EndAction()
    {
        follower.GetComponent<Follower>().RemoveAction();
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
