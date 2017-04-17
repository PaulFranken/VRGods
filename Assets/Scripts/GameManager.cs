using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public List<Resource> resources = new List<Resource>();
    private int stoneAmount = 0;
    private int woodAmount = 0;

	// Use this for initialization
	void Start ()
	{
	    Resource s = new StoneResource();

        Debug.Log(s.resourceWeight);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(resources[0].resourceType);
		//AddResource("Stone", 20);
	}

    public void AddResource(String type, int amount)
    {
        Debug.Log(type);
        if (type == "Stone")
        {
            stoneAmount += amount;
        }else if (type == "Wood")
        {
            woodAmount += amount;
        }
    }

    public void RemoveResource(String type, int amount)
    {
        if (type == "Stone")
        {
            stoneAmount -= amount;
        }
        else if (type == "Wood")
        {
            woodAmount -= amount;
        }
    }

    void OnGUI()
    {
        GUI.TextField(new Rect(10, 10, 100, 20), "Stone: " + stoneAmount);
        GUI.TextField(new Rect(10, 30, 100, 20), "Wood: " + woodAmount);
    }
}
