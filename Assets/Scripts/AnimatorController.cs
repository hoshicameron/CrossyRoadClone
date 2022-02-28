using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private Animator animator;
    private static readonly int Dead = Animator.StringToHash("dead");
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int JumpStart = Animator.StringToHash("jumpStart");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SetAnimation();

    }

    private void SetAnimation()
    {
        if (playerController.IsDead) animator.SetBool(Dead, true);

        if (playerController.IsJumping) animator.SetBool(Jump, true);
        else if (playerController.JumpStart) animator.SetBool(JumpStart, true);
        else
        {
            animator.SetBool(Jump, false);
            animator.SetBool(JumpStart, false);
        }
    }
}
