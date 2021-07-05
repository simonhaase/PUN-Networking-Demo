using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    public Transform[] spawnpoint;
    private void Awake()
    {
        Instance = this;
        spawnpoint = GetComponentsInChildren<Transform>();
    }

    public Transform GetSpawnpoint()
    {
        return spawnpoint[Random.Range(0, spawnpoint.Length)];
    }
}
