using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FPSAnimation2 : MonoBehaviour
{
    private GameObject handL;
    private Animator anim;
    public Animator enemyanim;
    private AnimatorStateInfo stateInfo;

    private ParticleSystem smoke;
    private AudioSource shoot;

    public static bool finishedReloading = true;

    public Text text;
    public Camera fpsCam;

    private PhotonView PV;

    public float damage = 10f;
    public float range = 100f;

    public int munition = 6;
    public int curmuniton;

    void Start()
    {        
        PV = gameObject.GetComponentInParent<PhotonView>();
        if (!PV.IsMine)
        {
            anim = gameObject.GetComponentInChildren<Animator>();
            this.enabled = false;
        }
        else
        {
            anim = gameObject.GetComponent<Animator>();
        }

        shoot = gameObject.GetComponent<AudioSource>();
        //handL = GameObject.Find("Hand L.001");
        smoke = gameObject.GetComponent<ParticleSystem>();

        curmuniton = munition;
        DisplayMunition();
    }

    void PlayShoot()
    {
        //Trigger from Animator

        if (curmuniton > 0 && finishedReloading)
        {
            curmuniton--;
            shoot.Play();
            Shoot();
            smoke.Play();
            DisplayMunition();
        }
        if (curmuniton <= 0 && finishedReloading)
        {
            anim.SetTrigger("Reload");
            finishedReloading = false;
            DisplayMunition();
        }
    }

    private void FinishedReloading()
    {
        curmuniton = munition;
        finishedReloading = true;
        DisplayMunition();
    }
    public void PlayAnimShoot()
    {
        if (finishedReloading)
        {
            anim.SetTrigger("Shoot");
            //handL.SetActive(false);
        }
    }

    public void PlayAnimReload()
    {
        finishedReloading = false;
        //handL.SetActive(true);
        anim.SetTrigger("Reload");
    }

    public void PlayAnimWalk()
    {
        //handL.SetActive(false);
        anim.SetBool("Run", true);
    }

    public void StopAnimWalk()
    {
        //handL.SetActive(false);
        anim.SetBool("Run", false);
    }

    void Shoot()
    {
        if (!PV.IsMine) return; //Only Player can shoot and inflict damage. gets synced over network

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            hit.collider?.GetComponent<IDamageable>()?.TakeDamage(damage);
            hit.collider?.GetComponent<ISpawnParticles>()?.SpawnParticles(hit.point);

            //target.ParticleHit(hit.point);
            //target.TakeDamage(damage);
 
        }
    }

    private void DisplayMunition()
    {
        Debug.Log(curmuniton.ToString());
        //text.text = "Muntion:" + curmuniton.ToString();
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
}
