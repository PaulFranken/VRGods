﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building {

    //public KeyValuePair<string, int> a = new KeyValuePair<string, int>();
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckIfDone();
	}

    public override void CheckIfDone()
    {
        foreach (Requirements r in requirementsList)
        {
            if (r.currentAmount < r.requiredAmount)
            {
                return;
            }
        }
        foreach (GameObject g in assignees)
        {
            g.GetComponent<Follower>().action.EndAction();
        }
    }
}
