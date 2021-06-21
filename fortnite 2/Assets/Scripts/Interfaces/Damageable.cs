using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
}

public interface ISpawnParticles
{
    void SpawnParticles(Vector3 hitpos);
}