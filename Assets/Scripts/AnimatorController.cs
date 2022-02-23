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
        SetRotation();
    }

    private void SetRotation()
    {
        if (!playerController.IsIdle || playerController.IsDead)    return;

        if(Input.GetKeyDown(KeyCode.UpArrow))    transform.rotation=Quaternion.Euler(270,0,0);
        if(Input.GetKeyDown(KeyCode.DownArrow))    transform.rotation=Quaternion.Euler(270,180,0);
        if(Input.GetKeyDown(KeyCode.RightArrow))    transform.rotation=Quaternion.Euler(270,90,0);
        if(Input.GetKeyDown(KeyCode.LeftArrow))    transform.rotation=Quaternion.Euler(270,-90,0);
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
