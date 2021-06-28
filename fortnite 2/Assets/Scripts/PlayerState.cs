using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour, IDamageable
{
    public float health = 100f;
    [SerializeField] private Text healthtext;
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        healthtext.text = "health: " + health.ToString();
    }

    public void TakeDamage(float amount)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, amount);
    }

    [PunRPC]
    private void RPC_TakeDamage(float amount)
    {
        health -= amount;

        if (PV.IsMine) 
        { 
        healthtext.text = "health: " + health.ToString();
        }

        if (health <= 0f)
        {
            PV.RPC("RPC_Die", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_Die()
    {
        Destroy(this.gameObject);
    }
}
