using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private float horizontal;
    private float vertical;
    public Vector2 Direction { get; private set; }
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Direction = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Direction * speed * Time.fixedDeltaTime);
    }
}
