using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour {

    public int health;
    public float speed;
    public int storageCapacity;
    public int itemAmount;
    public int currentStorage;
    public string followerName;
    public int strength;
    public int intellect;

    private bool canGather;
    private float gatherCooldown = 60;
    private bool isWalking = false;
    private bool isGathering = false;
    private bool isFull = false;

    private GameObject resourceTarget;
    private GameObject storagePoint;

    public Vector3 target;
    private NavMeshAgent agent;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update () {

        if (resourceTarget != null && isGathering)
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
        isGathering = false;
    }

    public void GatherResource(GameObject g)
    {
        isGathering = true;
        resourceTarget = g;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == resourceTarget)
        {
            Debug.Log(collision.gameObject.GetComponent<Resource>().resourceWeight);
            //storageCapacity += collision.gameObject.GetComponent<Resource>().resourceWeight;
            if (storageCapacity >= currentStorage + collision.gameObject.GetComponent<Resource>().resourceWeight)
            {
                itemAmount += 1;
                currentStorage += collision.gameObject.GetComponent<Resource>().resourceWeight;
                Debug.Log(itemAmount);
            }
            else
            {
                isFull = true;
                storagePoint = GameObject.FindGameObjectWithTag("Storage");
            }
        }
    }
}
