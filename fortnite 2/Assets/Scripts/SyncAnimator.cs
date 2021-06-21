using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SyncAnimator : StateMachineBehaviour
{

    private Sync3rdPlayerAnim syncanim;
    public string animationName;

    private void Start()
    {
        
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        syncanim = animator.GetComponentInParent<Sync3rdPlayerAnim>();

        if (animationName == "Shoot")
        {
            syncanim.Shoot();
        }
        else if (animationName == "Reload")
        {
            syncanim.Reload();
        }
        else if(animationName == "Run")
        {
            syncanim.StartRun();
        }
        
        else
        {
            Debug.Log("syncing animaiton failed");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationName == "Run")
        {
            syncanim.StopWalk();
        }
    }
}
