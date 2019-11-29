using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public GameObject standsOnLand { get; set; }
    public GameObject aimsAtLand { get; set; }
    private GameObject aimsAtLandOld;
    private GameObject standsOnLandOld;

    public GameObject WindSpawn;

    private GameObject tempWind = null;
    private GameObject ParentImage;

    private RaycastHit hit;
    private Vector3 landN = new Vector3(0, 0, 1);
    private int layerMask;

    void checkLandUnder()
    {
        Vector3 AimAt = transform.TransformDirection(Vector3.back);

        if (Physics.Raycast(transform.position, AimAt, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("HIT: " + hit.transform.gameObject.GetComponentInParent<Country>().getCountryCode());
            //Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            standsOnLand = hit.transform.gameObject;
            landN = hit.normal;
        }
        else
        {
           // Debug.DrawRay(transform.position, AimAt * 10.0f, Color.red);
            standsOnLand = null;
        }
    }

  //  Vector3 gizmoPos = new Vector3(0, 0, 0);
    List<Vector3> AimPositions = new List<Vector3>();

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(AimPositions != null)
            foreach(var pos in AimPositions)
            {
                Gizmos.DrawSphere(pos, 0.015f);
            }

    }*/

    void checkAimingOnLand()
    {



        if (standsOnLand != null)
        {
            //Debug.DrawRay(standsOnLand.transform.position, landN * 20.0f, Color.magenta);
            Vector3 AimAt = standsOnLand.transform.up;

            float angle = transform.eulerAngles.z - standsOnLand.transform.eulerAngles.z;
            angle -= 90.0f;
            AimAt = Quaternion.AngleAxis(angle, landN) * AimAt;
            Vector3 startPos = standsOnLand.transform.position;
         //   Debug.DrawRay(startPos, AimAt * 20.0f, Color.magenta);
            AimPositions.Clear();

            for(int i = 1; i < 25; i++)
            {
                Vector3 tmpPos = startPos + AimAt * (0.02f*i);
                AimPositions.Add(tmpPos);
            }

            foreach(var pos in AimPositions)
            {
               // Debug.DrawLine(pos - landN, pos + landN, Color.magenta);
                if (Physics.Linecast(pos - landN, pos + landN, out hit, layerMask))
                {
                    if(hit.collider.gameObject != standsOnLand)
                    {
                        //Debug.DrawLine(pos - landN, pos + landN, Color.magenta);
                        aimsAtLand = hit.collider.gameObject;
                        return;
                    }
                }
            }

        }


        /*aimsAtLand = null;
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
        }*/
    }

    void Start()
    {
        layerMask = LayerMask.GetMask("Country");
        tempWind = Instantiate(WindSpawn);
        tempWind.SetActive(false);
        ParentImage = GetComponentInParent<Transform>().gameObject;
    }

    void RegulateWind()
    {
        if(standsOnLand != null)
        {
            tempWind.SetActive(true);
            tempWind.transform.position = standsOnLand.transform.position;
            tempWind.transform.rotation = standsOnLand.transform.rotation;

            Vector3 AimAt = standsOnLand.transform.up;
            float angle = transform.eulerAngles.z - standsOnLand.transform.eulerAngles.z;
            angle += 180.0f;
            tempWind.transform.rotation = Quaternion.AngleAxis(angle, landN);
            /*var tmpRot = tempWind.transform.rotation;
            var tmpEuler = tmpRot.eulerAngles;
            tmpEuler.z += 90.0f;
            tmpEuler.z -= transform.eulerAngles.z;
            tempWind.transform.eulerAngles = tmpEuler;*/
        }
        else
        {
            tempWind.SetActive(false);
        }
    }

    private void swapLandColors()
    {
        if(aimsAtLand != aimsAtLandOld && aimsAtLandOld != null)
        {
            aimsAtLandOld.GetComponentInParent<MeshRenderer>().material.color = Color.yellow;
        }
        if (standsOnLand != standsOnLandOld && standsOnLandOld != null)
        {
            standsOnLandOld.GetComponentInParent<MeshRenderer>().material.color = Color.yellow;
        }
        if(aimsAtLand != null)
        {
            aimsAtLand.GetComponentInParent<MeshRenderer>().material.color = Color.red;
            if(standsOnLand == null)
                aimsAtLand.GetComponentInParent<MeshRenderer>().material.color = Color.yellow;
        }
        if (standsOnLand != null)
        {
            standsOnLand.GetComponentInParent<MeshRenderer>().material.color = Color.green;
        }
        

        aimsAtLandOld = aimsAtLand;
        standsOnLandOld = standsOnLand;
    }


    void Update()
    {
        checkLandUnder();
        RegulateWind();
        checkAimingOnLand();
        swapLandColors();


        // Vector3 AimAt = transform.TransformDirection(Vector3.right);
        // forceField.directionX = AimAt.x;
        // forceField.directionY = AimAt.z;
        // Debug.Log("TEST: " + transform.eulerAngles.z);

    }
    private int frames = 0;

    private void FixedUpdate()
    {
        frames = (frames + 1) % 20;

        if (standsOnLand && aimsAtLand && frames == 1)
        {
            Country c1 = standsOnLand.GetComponentInParent<Country>();
            Country c2 = aimsAtLand.GetComponentInParent<Country>();
            if (c1 == null)
                Debug.Log("NULL COUNTRY");
            else if(c1.NumParticles > 0)
            {
                c1.NumParticles--;
                c2.NumParticles++;
            }

        }
    }
}
