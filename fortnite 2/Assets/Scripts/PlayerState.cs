using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour, IDamageable
{
    public float maxhealth = 100f;
    private float currenthealth;
    [SerializeField] private Text healthtext;
    PhotonView PV;
    PlayerManager playerManager;

    private void Start()
    {
        currenthealth = maxhealth;
        PV = GetComponent<PhotonView>();
        healthtext.text = "health: " + maxhealth.ToString();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("Take damage" + amount);
        PV.RPC("RPC_TakeDamage", RpcTarget.All, amount);
    }

    [PunRPC]
    private void RPC_TakeDamage(float amount)
    {
        currenthealth -= amount;

        if (PV.IsMine) 
        { 
            healthtext.text = "health: " + currenthealth.ToString();
        }

        if (currenthealth <= 0f)
        {
            PV.RPC("RPC_Die", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_Die()
    {
        playerManager.Die();
    }
}
