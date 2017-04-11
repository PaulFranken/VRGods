using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Spell {

    public Vector3 origin;
    public Vector3 destination;
    public GameObject meteor;
    public bool isReady = false;
    float speed = 0f;
    float acceleration = 10f;
    Collider[] colliders;
    HitScript hitScript;


    public override void InitiateSpell()
    {
        isChannelling = true;
        hitScript = meteor.GetComponent<HitScript>();
    }

    public override void StartSpell()
    {
    }

    public override void StartSpell(RaycastHit target)
    {
        if(origin == Vector3.zero && target.collider.gameObject.tag == "Sky")
        {
            origin = target.point;
            Debug.Log("1");
        }
        else if(destination == Vector3.zero && origin != Vector3.zero)
        {
            destination = target.point;
            Debug.Log("2");
            meteor = (GameObject)Resources.Load("Prefabs/Meteor");
            meteor = Instantiate(meteor, origin, Quaternion.identity);
            isReady = true;
        }
        
    }

    public override void ChannelSpell()
    {
        if (isReady)
        {

            speed = speed + acceleration;
            
            Debug.Log("doe ittt");
            //meteor.transform.position = Vector3.Lerp(meteor.transform.position, destination, t);
            meteor.transform.position = Vector3.MoveTowards(meteor.transform.position, destination, speed * Time.deltaTime);
            if(meteor.transform.position == destination)
            {
                colliders = Physics.OverlapSphere(meteor.transform.position, 100f, 1);
                foreach (Collider c in colliders)
                {
                    if (c.gameObject.GetComponent<Rigidbody>())
                    {
                        if(c.gameObject.tag == "Building")
                        {
                            c.gameObject.GetComponent<BuildingInstructions>().GetDestroyed();
                        }
                        c.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, meteor.transform.position, 100f);

                    }
                }
            }
        }
    }

    public override void EndSpell()
    {
    }

    


}
