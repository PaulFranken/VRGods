using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour {

    public GameObject gameManager;
    public bool placingBuilding = false;
    private GameObject currentBuilding;
    private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
	}

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 300);
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.tag == "Resource")
            {
                Debug.Log("Resource hit");
                gameManager.GetComponent<FollowerManager>().GatherResource(hit.collider.gameObject);
            }
            else
            {
                gameManager.GetComponent<FollowerManager>().target = hit.point;
            }           
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentBuilding = (GameObject)Resources.Load("Prefabs/Building1");
            currentBuilding = Instantiate(currentBuilding, hit.point, Quaternion.identity);
            Debug.Log(currentBuilding.name);
        }
        if (currentBuilding)
        {
            if (hit.collider.gameObject.layer == 8)
            {
                targetPosition = hit.point;
                currentBuilding.transform.position = targetPosition;
                
            }
            else
            {
                targetPosition = Vector3.zero;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(currentBuilding);
            Debug.Log(targetPosition);
            if (currentBuilding && targetPosition != Vector3.zero)
            {
                currentBuilding = null;
            }
            
        }

        
    }


}
