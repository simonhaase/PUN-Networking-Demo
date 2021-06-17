using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync3rdPlayerAnim : MonoBehaviour
{

    private Animator anim;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        anim = enemy.GetComponentInChildren<Animator>();
    }

    public void Shoot()
    {
        anim.Play("Shoot");
    }

    public void Reload()
    {
        anim.Play("Reload");
    }

    public void StartRun()
    {
        anim.SetBool("Run", true);
    }

    public void StopWalk()
    {
        anim.SetBool("Run", false);
    }
}
