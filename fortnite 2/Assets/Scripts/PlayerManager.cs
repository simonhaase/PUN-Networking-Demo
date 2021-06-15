using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;

    void Start()
    {
        
        PV = GetComponent<PhotonView>();
        Debug.Log(PV.IsMine);
        if (PV.IsMine)
        {
            CreateControllerPlayer();
        }
        if (!PV.IsMine)
        {
            CreateControllerEnemy();
        }
    }

    void CreateControllerPlayer()
    {
        if (PV.IsMine)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(0f, 8f, 0f), Quaternion.identity); //Instatiate Player                                                                                                                                //PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "barrel"), new Vector3(0f, 8f, 0f), Quaternion.identity);
        }
        else if (!PV.IsMine)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerEnemy"), new Vector3(0f, 8f, 0f), Quaternion.identity);
        }
    }
    void CreateControllerEnemy()
    {
        if (PV.IsMine)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(0f, 8f, 0f), Quaternion.identity); //Instatiate Player                                                                                                                                //PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "barrel"), new Vector3(0f, 8f, 0f), Quaternion.identity);
        }
        else if (!PV.IsMine)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerEnemy"), new Vector3(0f, 8f, 0f), Quaternion.identity);
        }
    }
}
