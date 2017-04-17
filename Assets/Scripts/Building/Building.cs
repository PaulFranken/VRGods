using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {

    //public Dictionary<string, int> resourcesRequired = new Dictionary<string, int>();

    [System.Serializable]
    public class Requirements
    {
        public string type;
        public int requiredAmount;
        public int currentAmount;
    }
    public List<Requirements> requirementsList = new List<Requirements>();
    public List<GameObject> assignees = new List<GameObject>();

    void Start () {
		
	}
	
	void Update () {
		
	}

    public abstract void CheckIfDone();
}
