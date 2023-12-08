using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public int IfMovingHash;
    public int IfWalkingHash;
    public int IfSneakingHash;
    public int IfSprintingHash;
    public int IfJumpingHash;
    public int IfRollingHash;
    public int IfHidingHash;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        IfMovingHash = Animator.StringToHash("IfMoving");
        IfWalkingHash = Animator.StringToHash("IfWalking");
        IfSneakingHash = Animator.StringToHash("IfSneaking");
        IfSprintingHash = Animator.StringToHash("IfSprinting");
        IfJumpingHash = Animator.StringToHash("IfJumping");
        IfRollingHash = Animator.StringToHash("IfRolling");
        IfHidingHash = Animator.StringToHash("IfHiding");
    }

    private void Update()
    {
        bool IfWalking = animator.GetBool(IfMovingHash);
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool(IfMovingHash, true);

        }
        else{
            animator.SetBool(IfMovingHash, false);
        }
    }
}
