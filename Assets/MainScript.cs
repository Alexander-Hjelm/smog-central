using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
   // public GameObject FactorySpawn;
    private List<GameObject> lands;

    void Start()
    {
        lands = new List<GameObject>();

        
        foreach(GameObject land in lands)
        {
           // land.GetComponent<Country>().ParticleType = FactorySpawn;
        }
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

}
