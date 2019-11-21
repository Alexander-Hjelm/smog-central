using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleScript : MonoBehaviour
{
    private ParticleSystem partSys;
    private Country land;

    void Start()
    {
        partSys = GetComponent<ParticleSystem>();
        land = GetComponent<Country>();
    }

    void Update()
    {
        var emiss = partSys.emission;
        var main = partSys.main;
        main.maxParticles = 10;
        emiss.rateOverTime = 2.0f;

        if (land)
        {
            main.maxParticles = land.NumParticles;
            if(land.NumParticles >= 2)
            {
                emiss.rateOverTime = land.NumParticles / 2+1;
            }

        }

    }
}
