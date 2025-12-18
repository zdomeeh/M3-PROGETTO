using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector2 dir = playerController.Direction;
        bool isMoving = playerController.Direction != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);

        animator.SetFloat("HorizontalDirection", dir.x);

    }

    private void PlayDamage()
    {
        animator.SetTrigger("IsDamaged");
    }

    private void PlayDeath()
    { 
        animator.SetBool("IsDeath",true);
    }
}
