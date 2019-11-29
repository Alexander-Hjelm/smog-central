using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ParticleScript : MonoBehaviour
{
    // These constants control how many particles should spawn. Decrease for performance
    private const float MAX_NUM_OF_PARTICLES = 1000f;   // Hard cap of number of particles in the system
    private const float PARTICLE_NUM_CONVERSION_RATION = 0.00001f;  // Multiplier on the country's NumOfParticles

    private ParticleSystem partSys;
    private Country land;

    void Start()
    {
        partSys = GetComponent<ParticleSystem>();
        land = GetComponentInParent<Country>();

        // Set emission shape to the mesh renderer of the country
        ParticleSystem.ShapeModule shape = partSys.shape;
        shape.shapeType = ParticleSystemShapeType.MeshRenderer;
        shape.meshRenderer = land.GetComponent<MeshRenderer>();
        shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
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
            main.maxParticles = (int)Mathf.Min(land.NumParticles * PARTICLE_NUM_CONVERSION_RATION, MAX_NUM_OF_PARTICLES);
            if(land.NumParticles >= 2)
            {
                emiss.rateOverTime = land.NumParticles / 2+1;
            }

        }
        emiss.rateOverTime = 10;
        main.maxParticles = 50;



    }
}
