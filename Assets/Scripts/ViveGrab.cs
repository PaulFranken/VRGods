using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveGrab : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    // 1
    private GameObject collidingObject;
    // 2
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        // 1
        if(col.tag == "Grabbable")
        {
            collidingObject = col.gameObject;
        }        
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        // 1
        if (collidingObject)
        {
            objectInHand = collidingObject;
            collidingObject = null;
            // 2
            var joint = AddFixedJoint();
            joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
            //objectInHand.GetComponent<Rigidbody>().useGravity = false;
            objectInHand.transform.parent = this.transform;
            objectInHand.GetComponent<Rigidbody>().isKinematic = true;

        }

    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3


            //objectInHand.GetComponent<Rigidbody>().useGravity = true;
            objectInHand.transform.parent = null;

            objectInHand.GetComponent<Rigidbody>().isKinematic = false;


            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
            objectInHand.GetComponent<Rigidbody>().velocity *= 20;
            objectInHand.GetComponent<Rigidbody>().angularVelocity *= 20;


        }
        // 4
        objectInHand = null;
        Debug.Log("weg");
    }

    // Update is called once per frame
    void Update () {
        // 1
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // 2
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
