﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public GameObject standsOnLand { get; set; }
    public GameObject aimsAtLand { get; set; }
    public GameObject WindSpawn;

    private GameObject tempWind = null;

    private RaycastHit hit;
    private int layerMask;

    void checkLandUnder()
    {
        Vector3 AimAt = transform.TransformDirection(Vector3.back);

        if (Physics.Raycast(transform.position, AimAt, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("HIT: " + hit.transform.gameObject.GetComponentInParent<Country>().getCountryCode());
            //Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            standsOnLand = hit.transform.gameObject;
        }
        else
        {
            Debug.DrawRay(transform.position, AimAt * 10.0f, Color.red);
            standsOnLand = null;
        }
    }

    void checkAimingOnLand()
    {
        aimsAtLand = null;

        Vector3 FromPos = transform.position;
        FromPos.y += 1.0f;
        Vector3 AimAt = transform.TransformDirection(Vector3.right);
        AimAt.y = -4.0f;
        AimAt.Normalize();

        float yPiece = 1.0f / 80.0f;

        for (int i = 0; i < 80; i++)
        {
            AimAt.y += yPiece;
            Debug.DrawRay(FromPos, AimAt * 5.0f, Color.magenta);
            if (Physics.Raycast(FromPos, AimAt, out hit, Mathf.Infinity, layerMask))
            {
                aimsAtLand = hit.transform.gameObject;
                return;
            }
        }
    }

    void Start()
    {
        layerMask = LayerMask.GetMask("Country");
        tempWind = Instantiate(WindSpawn);
        tempWind.SetActive(false);
    }

    void RegulateWind()
    {
        if(standsOnLand != null)
        {
            tempWind.SetActive(true);
            tempWind.transform.position = standsOnLand.transform.position;
            tempWind.transform.rotation = transform.rotation;
        }
        else
        {
            tempWind.SetActive(false);
        }
    }


    void Update()
    {
        checkLandUnder();
        RegulateWind();
    //    checkAimingOnLand();
    // Vector3 AimAt = transform.TransformDirection(Vector3.right);
    // forceField.directionX = AimAt.x;
    // forceField.directionY = AimAt.z;

    }
    private int frames = 0;

    private void FixedUpdate()
    {
        frames = (frames + 1) % 20;

        if (standsOnLand && aimsAtLand && frames == 1)
        {
            Country c1 = standsOnLand.GetComponent<Country>();
            Country c2 = aimsAtLand.GetComponent<Country>();
            if(c1.NumParticles > 0)
            {
                c1.NumParticles--;
                c2.NumParticles++;
            }

        }
    }
}
