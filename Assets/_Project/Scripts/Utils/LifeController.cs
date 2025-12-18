using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife = 100; // Vita massima
    private int currentLife;

    private void Awake()
    {
        currentLife = maxLife; // Imposta la vita iniziale
    }

    // Funzione per ricevere danno
    public void TakeDamage(int amount)
    {
        currentLife -= amount;

        if (currentLife <= 0)
        {
            Die();
        }
    }

    // Funzione che distrugge il GameObject quando la vita finisce
    private void Die()
    {
        Destroy(gameObject);
    }

    // Getter pubblico per leggere la vita da altri script
    public int CurrentLife => currentLife;
}
