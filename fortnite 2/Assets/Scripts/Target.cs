using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Target : MonoBehaviour, IDamageable, ISpawnParticles
{
    public float health = 50f;
    public GameObject hitParticle;
    private MeshRenderer mesh;
    private PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        mesh = GetComponent<MeshRenderer>();
    }

    [PunRPC]
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {            
            PV.RPC("RPC_Die", RpcTarget.All);           
        }
    }

    [PunRPC]
    void RPC_Die()
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
    public void SpawnParticles(Vector3 hitpos)
    {
        Instantiate(hitParticle, transform, false);

        if (hitParticle.GetComponent<ParticleSystem>())
        {
            Debug.Log("particle found");
            var ps = hitParticle.GetComponent<ParticleSystem>();
            var hitp = hitParticle.GetComponent<HitParticle>();
            hitp.position = transform.InverseTransformPoint(hitpos);
            hitp.SetHitPosition();
            hitp.PlayParticles();
        }
    }
}