using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour //All countries have this script
{

    public int NumParticles { get; set; }
    public float Area { get; set; }
    private HashSet<GameObject> smogs;
    public int Production { get; set; }
    private int randNum;
    public GameObject ParticleType { get; set; }

    public void AddSmog(GameObject smog) //If a particle raycasts into the country, call this foo
    {
        smogs.Add(smog);
        NumParticles = smogs.Count;
    }
    public void RemoveSmog(GameObject smog)
    {
        smogs.Remove(smog);
        NumParticles = smogs.Count;
    }

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
        smogs = new HashSet<GameObject>();
    }

    private void FixedUpdate()
    {
      
    }

    private void Update()
    {
        randNum = Random.Range(1, 100);
        if (randNum <= Production && ParticleType)
        {
            var particle = Instantiate(ParticleType);
        }
    }


}
