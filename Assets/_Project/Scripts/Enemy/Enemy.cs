using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;       // Velocità del nemico
    [SerializeField] private int damageToPlayer = 1; // Danno inflitto al Player

    private Transform player;
    private LifeController playerLife;
    private Rigidbody2D rb;
    private EnemyAnimation enemyAnim;

    public Vector2 Direction { get; private set; } // Direzione verso il Player

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Recupera Rigidbody2D dal Inspector
        enemyAnim = GetComponent<EnemyAnimation>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // Trova il Player nella scena
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerLife = playerObj.GetComponent<LifeController>();
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            // Movimento verso il Player
            Vector2 dir = (player.position - transform.position).normalized;
            Direction = dir;
            rb.velocity = dir * speed;
        }
        
        else
        {
            rb.velocity = Vector2.zero; // Se il Player non c'è
            Direction = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se collide con il Player, infligge danno
        if (collision.gameObject.CompareTag("Player") && playerLife != null)
        {
            playerLife.TakeDamage(damageToPlayer);

            // Chiama animazione di morte
            if (enemyAnim != null) enemyAnim.PlayDeath();

            Destroy(gameObject, 0.5f);
        }
    }
}
