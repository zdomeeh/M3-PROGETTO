using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife = 100; // Vita massima
    private int currentLife;

    private PlayerAnimation playerAnimation;
    private bool isDead = false; // Stato di morte

    private void Awake()
    {
        currentLife = maxLife; // Imposta la vita iniziale
        playerAnimation = GetComponent<PlayerAnimation>(); // Recupera PlayerAnimation sullo stesso GameObject
    }

    // Funzione per ricevere danno
    public void TakeDamage(int amount)
    {
        if (isDead) return; // Se è già morto, ignora ulteriori danni

        currentLife -= amount;

        // Fa partire l'animazione di danno
        if (playerAnimation != null)
        {
            playerAnimation.PlayDamage();
        }

        AudioManager playerAudio = GetComponent<AudioManager>();

        if (playerAudio != null)
        {
            playerAudio.PlayDamage();
        }

        if (currentLife <= 0)
        {
            Die();
        }
    }

    // Funzione che distrugge il GameObject quando la vita finisce
    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // Blocca la fisica per evitare spinta dei nemici
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;   // Ferma movimento
            rb.isKinematic = true;        // Blocca la fisica
        }

        // Disattiva tutti i collider
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        // Animazione di morte per il Player
        if (playerAnimation != null)
        {
            playerAnimation.PlayDeath();
            Destroy(gameObject, 3f); // il Player rimane 3 secondi per l'animazione prima di distruggersi
        }
        else
        {
            EnemyDrop drop = GetComponent<EnemyDrop>();
            if (drop != null)
            {
                drop.TryDrop();
            }
            Destroy(gameObject); // Destroy istantaneo per il nemico
        }

        AudioManager playerAudio = GetComponent<AudioManager>(); 

        if (playerAudio != null)
        {
            playerAudio.PlayDeath();
        }
    }


    // Getter per controllare se il player è morto
    public bool IsDead => isDead;

    // Getter pubblico per leggere la vita da altri script
    public int CurrentLife => currentLife;
}
