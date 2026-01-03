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

        // Fai partire l'animazione di danno
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
            rb.isKinematic = true;

        // Fai partire l'animazione di morte se esiste
        if (playerAnimation != null)
        {
            playerAnimation.PlayDeath();
        }

        AudioManager playerAudio = GetComponent<AudioManager>();

        if (playerAudio != null)
        {
            playerAudio.PlayDeath();
        }
        // Distruzione del GameObject con un piccolo delay per far vedere l'animazione
        Destroy(gameObject, 3f);
    }


    // Getter per controllare se il player è morto
    public bool IsDead => isDead;

    // Getter pubblico per leggere la vita da altri script
    public int CurrentLife => currentLife;
}
