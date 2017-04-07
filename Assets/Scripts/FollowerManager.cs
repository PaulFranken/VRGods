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

        if (target != null)
        {
            foreach (var f in followerList)
            {
                f.GetComponent<Follower>().SetTarget(target);
            }
        }
		
	}
}
