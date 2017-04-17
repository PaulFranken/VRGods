using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Resources;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour {

    public float health;
    public float speed;

    public Resource resource;
    public int storageCapacity;
    public int itemAmount;
    public int currentLoad;
    public GameObject currentCollidingObject;

    public string followerName;
    public int strength;
    public int intellect;

    private bool hasAction = false;
    

    private GameObject resourceTarget;

    public Vector3 target;
    public NavMeshAgent agent;
    public Action action;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        resource = new StoneResource();
    }

    // Update is called once per frame
    void Update () {
        if (hasAction)
        {
            action.PerformAction();
        }
        else
        {
            
        }

    }

    public void SetAction(string actionType, Vector3 target, GameObject targetGameObject)
    {
        
        
        switch (actionType)
        {
            case "Move":
                this.action = new MoveAction(this.gameObject, target);
                this.hasAction = true;
                break;
            case "Resource":
                if (action != null)
                {
                    this.action.EndAction();
                    Debug.Log("ENNDDDDD");
                }
                this.action = new GatherAction(this.gameObject, targetGameObject);
                this.hasAction = true;
                break;
            case "Build":
                this.action = new BuildAction(this.gameObject, targetGameObject);
                this.hasAction = true;
                break;
            default:
                break;
        }
    }

    public void RemoveAction()
    {

        this.action = null;
        this.hasAction = false;
    }

    private void OnCollisionStay(Collision other)
    {

        if (other.gameObject.tag != "Ground" && other.gameObject.tag != "Follower")
        {
            this.currentCollidingObject = other.gameObject;
        }
    }

    
}
