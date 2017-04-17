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
    public GameObject brokenBuilding;

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
        
        if (tempVel.x > 25f || tempVel.x < -25f || tempVel.y < -25f || tempVel.z > 25f || tempVel.z < -25f)
        {


            Instantiate(brokenBuilding, this.transform.position, Quaternion.identity);
            col.gameObject.GetComponent<Rigidbody>().velocity = tempVel;
            Destroy(this.gameObject);
        }
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
