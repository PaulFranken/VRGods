using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    PlayerScript playerScript;
    // 1
    public GameObject laserPrefab;
    // 2
    private GameObject laser;
    // 3
    private Transform laserTransform;
    // 4
    private Vector3 hitPoint;

    private GameObject currentBuilding;

    public GameObject gameManager;

    private bool placingBuilding = false;

    private bool showLaser = false;

    private bool pointing = false;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }


    private void Start()
    {
        // 1
        laser = Instantiate(laserPrefab);
        // 2
        laserTransform = laser.transform;

        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update() {

        RaycastHit hit;

        if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 1000)) {
            hitPoint = hit.point;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Axis0))
        {
            Vector2 touchpad = (Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));

            if (touchpad.y > 0.7f)
            {
                if (currentBuilding)
                {
                    currentBuilding.GetComponent<BuildingInstructions>().PlaceFoundations();
                    gameManager.GetComponent<FollowerManager>().AssignAction("Build", Vector3.zero, currentBuilding);
                    currentBuilding = null;
                    placingBuilding = false;
                }
                else
                {
                    placingBuilding = true;
                    currentBuilding = (GameObject)Resources.Load("Prefabs/Building1");
                    currentBuilding = Instantiate(currentBuilding, hitPoint, Quaternion.identity);
                }
            }
        }

        if (placingBuilding)
        {

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 200))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    currentBuilding.transform.position = hitPoint;
                }

                ShowLaser(hit);
                currentBuilding.gameObject.transform.position = hitPoint;
            }
        }

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            ShowLaser(hit);
        }

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            pointing = true;
            hitPoint = hit.point;
            ShowLaser(hit);

        }
        else if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (playerScript.spell.isChannelling)
            {
                pointing = true;
                
                playerScript.spell.StartSpell(hit);
                
            }
            if (hit.collider.gameObject.tag == "Resource")
            {
                gameManager.GetComponent<FollowerManager>().AssignAction("Resource", Vector3.zero, hit.collider.gameObject);
            }
            else if (hitPoint != Vector3.zero)
            {
                gameManager.GetComponent<FollowerManager>().AssignAction("Move", hitPoint, null);
                //gameManager.GetComponent<FollowerManager>().target = hitPoint;
            }

            pointing = false;
        }
        else
        {
            pointing = false;
        }

        if(placingBuilding || pointing)
        {
            ShowLaser(hit);
        }
        else
        {
            laser.SetActive(false);
        }
        

    }
}
