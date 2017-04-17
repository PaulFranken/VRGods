using UnityEngine;



public abstract class Resource : MonoBehaviour {


    public virtual int resourceAmount { get; set;}
    public virtual string resourceType { get; set; }
    public virtual int resourceWeight { get; set; }


    // Use this for initialization
    void Start () {
		
    }
	
    // Update is called once per frame
    void Update () {
		
    }

    
}

