using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public GameObject standsOnLand { get; set; }
    public GameObject aimsAtLand { get; set; }
    

    void checkLandUnder()
    {
        int layerMask = LayerMask.GetMask("Country");
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            standsOnLand = hit.transform.gameObject;
        }
        else
        {
            standsOnLand = null;
        }
    }

    void checkAimingOnLand()
    {
        int layerMask = LayerMask.GetMask("Country");
        RaycastHit hit;
        aimsAtLand = null;

        for(int i = 0; i < 80; i++)
        {

        }


    }

    void Start()
    {
        
    }


    void Update()
    {
        checkLandUnder();
        

    }
}
