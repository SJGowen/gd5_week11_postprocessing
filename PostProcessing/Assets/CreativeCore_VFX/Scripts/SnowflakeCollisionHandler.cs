using UnityEngine;
using System.Collections.Generic;

public class SnowflakeCollisionHandler: MonoBehaviour
{
    private ParticleSystem ps;
    public LayerMask groundLayer;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> particles = new ();
        int numEntered = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < numEntered; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            particle.angularVelocity3D = Vector3.zero;
            particles[i] = particle;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
