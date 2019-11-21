using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FanInCollider : MonoBehaviour {

    HashSet<GameObject> parts;
    const float maxSpeed = 10.0f;
    float fanMaxForce = 1.0f;

    void Start()
    {
        parts = new HashSet<GameObject>();
    }

    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("TESTTT");
    }

    private void OnParticleTrigger()
    {
        Debug.Log("TRIGGG");
    }

    private void FixedUpdate()
    {

        float yAng = transform.rotation.eulerAngles.y+180.0f;

        foreach (var p in parts)
        {

            var rig = p.GetComponent<Rigidbody>();
            var tmpDir = Quaternion.Euler(0.0f, yAng, 0.0f) * Vector3.forward;
            //Change  tmpDir here relative to dir.
            rig.AddForce(fanMaxForce * tmpDir);
            if (rig.velocity.magnitude > maxSpeed)
            {
                rig.velocity = rig.velocity.normalized * maxSpeed;
            }

        }
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HEHEHE");
        if (other.gameObject.GetComponent<Rigidbody>() != null)
            parts.Add(other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {
        parts.Remove(other.gameObject);
    }
}
