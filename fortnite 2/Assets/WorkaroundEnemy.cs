using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkaroundEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject capsule;
    public GameObject hand;

    void Start()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            capsule.SetActive(true);
            hand.SetActive(false);
        }
    }
}
