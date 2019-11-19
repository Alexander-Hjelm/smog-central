using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    //float ang = 0;
    float rotateSpeed = 40.0f;
    int delayer = 0;

    private void FixedUpdate()
    {
        Quaternion rot = transform.rotation;
        rot *= Quaternion.Euler(Time.fixedDeltaTime* rotateSpeed, 0, 0);
        transform.rotation = rot;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
