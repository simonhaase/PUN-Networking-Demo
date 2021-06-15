using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    private ParticleSystem particle;
    public Vector3 position;

    public void SetHitPosition()
    {
        particle = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule _editableShape = particle.shape;
        _editableShape.position = position;
       
    }
    public void PlayParticles()
    {
        particle.Play();
    }
 
}
