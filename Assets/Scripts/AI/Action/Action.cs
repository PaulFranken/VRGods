using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Action : MonoBehaviour {

    public GameObject follower;

    public GameObject targetGameObject;

    public Vector3 targetPosition;

    public NavMeshAgent followerAgent;

    public abstract void StartAction(GameObject g);

    public abstract void StartAction(Vector3 t);

    public abstract void PerformAction();

    public abstract void EndAction();

	
}
