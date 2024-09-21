using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : MonoBehaviour, ISetupAbility
{

    private ParticleSystem particle;
    private ParticleSystem.EmissionModule emission;

    private void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();

        emission = particle.emission;

        particle.Stop();

        emission.enabled = false;
    }

    public void ActivateAbility(Vector3 position = default, Quaternion rotation = default)
    {

        Debug.Log("Dragon breath activated: " + particle);

        particle.Play();

        emission.enabled = true;
    }

    public void DeactivateAbility()
    {

        particle.Stop();

        emission.enabled = false;
    }
}
 