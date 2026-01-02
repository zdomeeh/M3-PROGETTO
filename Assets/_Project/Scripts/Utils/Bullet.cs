using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 2f;

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Recupera il Rigidbody2D presente sul GameObject
    }

    public void SetDirection(Vector2 dir) // Metodo pubblico chiamato dalla Gun subito dopo l'istanza del proiettile, imposto la direzione di movimento
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Distrugge automaticamente il proiettile dopo lifeTime secondi
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);  // Usa MovePosition così funziona correttamente con i collider degli enemy e Tilemap
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LifeController life = collision.GetComponent<LifeController>(); // Controlla se l'oggetto colpito ha un LifeController

        if (life != null) // Se presente, applica il danno
        {
            life.TakeDamage(damage);
        }

        Destroy(gameObject); // Il proiettile viene distrutto dopo l'impatto
    }
}
