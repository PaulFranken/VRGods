using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour {

    private GameObject[] followerList;
    public Vector3 target;

	// Use this for initialization
	void Start () {
        followerList = GameObject.FindGameObjectsWithTag("Follower");
        Debug.Log(followerList.Length);
	}
	
	// Update is called once per frame
	void Update () {

        //if (target != Vector3.zero)
        //{
        //    foreach (var f in followerList)
        //    {
        //        f.GetComponent<Follower>().SetTarget(target);
        //    }
        //}
		
	}

    public void AssignAction(string actionType, Vector3 target, GameObject targetGameObject)
    {

        if(target != Vector3.zero && target != null)
        {
            foreach (var f in followerList)
            {
                f.GetComponent<Follower>().SetAction("Move", target, targetGameObject);
            }
        }
    }

    public void GatherResource(GameObject g)
    {
        foreach (var f in followerList)
        {
            f.GetComponent<Follower>().GatherResource(g);
        }
    }
}
