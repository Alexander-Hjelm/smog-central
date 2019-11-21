﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
        {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);


        Debug.DrawLine(transform.position, fwd, Color.green);

        if (Physics.Raycast(transform.position, fwd, 10))
        {
            print("There is something in front of the object!");
            Debug.Log("asd");

        }
            
            
    }
}