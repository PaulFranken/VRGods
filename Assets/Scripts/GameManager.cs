using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public List<Resource> resources = new List<Resource>();


	// Use this for initialization
	void Start ()
	{
	    StoneResource s = new StoneResource();

        Debug.Log(s);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(resources[0].resourceType);
		AddResource("Stone", 20);
	}

    public void AddResource(String type, int amount)
    {
//        Resource res = (Resource)resources.Find(item => item.resourceType == type);
//        res.resourceAmount += amount;
    }
}
