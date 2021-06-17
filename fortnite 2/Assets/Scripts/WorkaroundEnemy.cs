using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorkaroundEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject enemy;

    void Start()
    {
        if (!GetComponentInChildren<PhotonView>().IsMine) //disable all player meshes, & animatorview when enemy prefab is loaded
        {
            SkinnedMeshRenderer[] mesh = player.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < mesh.Length; i++)
            {
                mesh[i].enabled = false;
            }
            //player.SetActive(false);

            Destroy(GetComponentInChildren<Rigidbody>());
            Debug.Log("enemy spawned");
        }

        else if (GetComponentInChildren<PhotonView>().IsMine)
        {
            enemy.SetActive(false);
            Debug.Log("player spawned");
        }

        else
        {
            Debug.Log("No PV detected. 1st Person");
            enemy.SetActive(false);
        }
    }
}
