using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private float horizontal;
    private float vertical;
    public Vector2 Direction { get; private set; }
    private Rigidbody2D rb;
    private LifeController lifeController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeController = GetComponent<LifeController>();
    }

    private void Update()
    {
        if (lifeController != null && lifeController.IsDead)
        {
            Direction = Vector2.zero; // ferma il movimento
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Direction = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate()
    {
        if (lifeController != null && lifeController.IsDead) return; // blocca movimento se morto

        rb.MovePosition(rb.position + Direction * speed * Time.fixedDeltaTime);
    }
}
