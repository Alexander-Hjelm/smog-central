using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour //All countries have this script
{

    public int NumParticles { get; set; }
    public float Area { get; set; }
    public int Production { get; set; }
    private int randNum;
  //  public GameObject FactoryVisualPrefab;
  //  private GameObject FactoryVis;

    public float GetCO2()
    {
        if(NumParticles == 0)
        {
            return 0;
        }
        return NumParticles/Area;
    }

    void Start()
    {
        Production = 1;
      //  FactoryVis = Instantiate(FactoryVisualPrefab, transform);
     //   FactoryVis.transform.localPosition = new Vector3(0, 0, 0);
        //FactoryVis.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void FixedUpdate()
    {
      
    }

    private void Update()
    {
        randNum = Random.Range(1, 100);
        if (randNum <= Production)
        {
            NumParticles++;
        }
    }


}
