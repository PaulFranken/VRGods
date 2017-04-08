using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.relativeVelocity);
        Vector3 tempVel = col.relativeVelocity;
        
        if (col.relativeVelocity.x > 10f || col.relativeVelocity.x < -10f || col.relativeVelocity.y < -10f || col.relativeVelocity.z > 10f || col.relativeVelocity.z < -10f)
        {
            

            this.GetComponent<BoxCollider>().enabled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<Rigidbody>().useGravity = true;
                child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
        }
        col.gameObject.GetComponent<Rigidbody>().velocity = tempVel * 0.3f;
    }
}
