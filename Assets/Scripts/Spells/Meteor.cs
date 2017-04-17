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
    float timer = 0f;
    bool hasLanded = false;


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

            speed = speed + (acceleration / 2);
            
            Debug.Log("doe ittt");

            timer += Time.deltaTime;

            if(timer > 2f)
            {
                meteor.transform.position = Vector3.MoveTowards(meteor.transform.position, destination, speed * Time.deltaTime);

            }

            //meteor.transform.position = Vector3.Lerp(meteor.transform.position, destination, t);
            if (meteor.transform.position == destination && hasLanded == false)
            {
                hasLanded = true;
                colliders = Physics.OverlapSphere(meteor.transform.position, 100f);
                foreach (Collider c in colliders)
                {
                    Debug.Log(c.gameObject.name);
                    if (c.gameObject.GetComponent<Rigidbody>())
                    {
                        if(c.gameObject.tag == "Building")
                        {
                            Debug.Log(Vector3.Distance(transform.position, c.gameObject.transform.position));
                            c.gameObject.GetComponent<BuildingInstructions>().GetDestroyed();
                        }
                        c.gameObject.GetComponent<Rigidbody>().AddExplosionForce(3000f, meteor.transform.position, 50f);

                    }
                }
                EndSpell();
            }
        }
    }

    public override void EndSpell()
    {
        GameObject.Find("Player").GetComponent<PlayerScript>().spell = null;
    }

    


}
