using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;
    //public int IfIdleHash;
    public int IfWalkingHash;
    public int IfSprintingHash;
    public int IfTurningLeftHash;
    public int IfTurningRightHash;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //IfIdleHash = Animator.StringToHash("IfIdle");
        IfWalkingHash = Animator.StringToHash("IsWalking");
        IfSprintingHash = Animator.StringToHash("IsRunning");
        IfTurningLeftHash = Animator.StringToHash("IsTurningLeft");
        IfTurningRightHash = Animator.StringToHash("IsTurningRight");
    }

    private void Update()
    {
        
    }
}
