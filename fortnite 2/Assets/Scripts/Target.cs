using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject hitParticle;
    private MeshRenderer mesh;

    void Start()
    {

        mesh = GetComponent<MeshRenderer>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        ParticlesExplosion();
        Destroy(mesh);
        var col = GetComponent<Collider>();
        Destroy(col);
        Destroy(gameObject, 2f);
    }

    void ParticlesExplosion()
    {
        var ps = GetComponentsInChildren<ParticleSystem>();
        foreach (var p in ps)
        {
            p.Play();
        }
    }
    public void ParticleHit(Vector3 hitpos)
    {
        Instantiate(hitParticle, transform, false);

        if (hitParticle.GetComponent<ParticleSystem>())
        {
            var ps = hitParticle.GetComponent<ParticleSystem>();
            var hitp = hitParticle.GetComponent<HitParticle>();
            hitp.position = transform.InverseTransformPoint(hitpos);
            hitp.SetHitPosition();
            hitp.PlayParticles();
        }
        Debug.Log("Spark");
    }
}