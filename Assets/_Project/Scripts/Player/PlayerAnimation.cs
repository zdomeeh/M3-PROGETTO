using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    [SerializeField] private string verticalDirName = "VerticalDirection";
    [SerializeField] private string horizontalDirName = "HorizontalDirection";
    private Vector2 lastDir = Vector2.zero;

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

        if (dir != Vector2.zero)
        {
          lastDir = dir;
        }
        animator.SetFloat(verticalDirName, lastDir.y);
        animator.SetFloat(horizontalDirName, lastDir.x);

    }

    public void PlayDamage()
    {
        animator.SetTrigger("IsDamaged");
    }

    public void PlayDeath()
    { 
        animator.SetBool("IsDeath",true);
    }
}
