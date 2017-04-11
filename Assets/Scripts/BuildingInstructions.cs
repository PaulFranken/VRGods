using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingInstructions : MonoBehaviour {

    
    public bool isFinished = false;
    private bool hasChanged = false;
    public bool isPlaced = false;
    private GameObject finishedBuilding;
    private GameObject unfinishedBuilding;

	// Use this for initialization
	void Start () {
        finishedBuilding = transform.FindChild("FinishedBuilding").gameObject;
        unfinishedBuilding = transform.FindChild("UnfinishedBuilding").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (isFinished && !hasChanged)
        {
            FinishBuilding();
            hasChanged = true;
        }

    }

    public void PlaceFoundations()
    {
        unfinishedBuilding.SetActive(true);
        finishedBuilding.SetActive(false);
        GetComponent<BoxCollider>().enabled = true;
    }

    public void FinishBuilding()
    {
        unfinishedBuilding.SetActive(false);
        finishedBuilding.SetActive(true);
        foreach (Transform t in finishedBuilding.transform)
        {
            t.GetComponent<BoxCollider>().enabled = true;
        }
        GetComponent<BoxCollider>().enabled = true;

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.relativeVelocity);
        Vector3 tempVel = col.relativeVelocity;
        
        if (col.relativeVelocity.x > 10f || col.relativeVelocity.x < -10f || col.relativeVelocity.y < -10f || col.relativeVelocity.z > 10f || col.relativeVelocity.z < -10f)
        {
            

            this.GetComponent<BoxCollider>().enabled = false;
            foreach (Transform child in transform.FindChild("FinishedBuilding"))
            {
                child.gameObject.GetComponent<Rigidbody>().useGravity = true;
                child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
        }
        col.gameObject.GetComponent<Rigidbody>().velocity = tempVel * 0.3f;
        Destroy(GetComponent<NavMeshObstacle>());
    }

    public void GetDestroyed()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        foreach (Transform child in transform.FindChild("FinishedBuilding"))
        {
            child.gameObject.GetComponent<Rigidbody>().useGravity = true;
            child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
