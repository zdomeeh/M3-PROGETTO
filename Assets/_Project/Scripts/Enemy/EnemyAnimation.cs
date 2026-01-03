using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private Enemy enemy;

    [SerializeField] private string verticalDirName = "VerticalDirection";
    [SerializeField] private string horizontalDirName = "HorizontalDirection";

    private Vector2 lastDir = Vector2.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Vector2 dir = enemy.Direction;
        bool isMoving = dir != Vector2.zero;

        animator.SetBool("IsMoving", isMoving);

        if (dir != Vector2.zero)
            lastDir = dir;

        animator.SetFloat(verticalDirName, lastDir.y);
        animator.SetFloat(horizontalDirName, lastDir.x);
    }

   // public void PlayDeath()
   // {
   //     animator.SetBool("IsDead", true);
  //  }
}

