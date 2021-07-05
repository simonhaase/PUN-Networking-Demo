using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;
    GameObject controller;

    void Start()
    {

        PV = GetComponent<PhotonView>();
        Debug.Log(PV.IsMine);
        if (PV.IsMine)
        {
            CreateControllerPlayer();
        }
    }

    void CreateControllerPlayer()
    {
        if (PV.IsMine)
        {
            Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnpoint.position, Quaternion.identity, 0, new object[] { PV.ViewID });
        }
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateControllerPlayer();
    }
}
