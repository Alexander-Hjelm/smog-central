using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ParticleScript : MonoBehaviour
{
    private ParticleSystem partSys;
    private Country land;

    void Start()
    {
        partSys = GetComponent<ParticleSystem>();
        land = GetComponentInParent<Country>();
    }


    void Update()
    {
        /*ParticleSystem.Particle[] emittedParticles = new ParticleSystem.Particle[partSys.particleCount];
        var parts = partSys.GetParticles(emittedParticles);
        for(int i = 0; i < parts; i++)
        {
            var tmpVel = emittedParticles[i].velocity;
            tmpVel.x = -50;
            emittedParticles[i].velocity = tmpVel;
        }
        partSys.SetParticles(emittedParticles, emittedParticles.Length);*/


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
