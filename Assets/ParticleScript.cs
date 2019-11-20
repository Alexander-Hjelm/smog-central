using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    private GameObject latestLand = null;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.green);

        int layerMask = LayerMask.GetMask("Country");
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            latestLand = hit.transform.gameObject;
            latestLand.GetComponent<Country>().AddSmog(gameObject);
        }
        else
        {
            if (latestLand)
            {
                latestLand.GetComponent<Country>().RemoveSmog(gameObject);
            }
        }

    }
}
