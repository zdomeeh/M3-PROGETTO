using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife = 100; // Vita massima
    private int currentLife;

    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        currentLife = maxLife; // Imposta la vita iniziale
        playerAnimation = GetComponent<PlayerAnimation>(); // Recupera PlayerAnimation sullo stesso GameObject
    }

    // Funzione per ricevere danno
    public void TakeDamage(int amount)
    {
        currentLife -= amount;

        // Fai partire l'animazione di danno se esiste
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
        Destroy(gameObject, 0.5f);
    }

    // Getter pubblico per leggere la vita da altri script
    public int CurrentLife => currentLife;
}
