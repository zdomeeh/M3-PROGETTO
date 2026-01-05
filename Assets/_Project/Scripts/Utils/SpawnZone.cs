using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesToSpawn; // Prefab nemici da spawnare
    [SerializeField] private int numberToSpawn = 3;       // Quanti nemici spawnare
    [SerializeField] private bool spawnOnce = true;       // Spawn solo una volta
    [SerializeField] private float respawnCooldown = 5f;  // Tempo in secondi tra spawn multipli

    private bool hasSpawned = false; // Stato per spawnOnce
    private float lastSpawnTime = -Mathf.Infinity; // Timestamp ultimo spawn per gestire il cooldown

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TrySpawn();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !spawnOnce)
        {
            TrySpawn();
        }
    }

    private void TrySpawn()
    {
        if (spawnOnce && hasSpawned) return; // Blocca spawn se spawnOnce e già spawnato
        if (Time.time < lastSpawnTime + respawnCooldown) return; // Blocca spawn se ancora in cooldown

        SpawnEnemies();
        lastSpawnTime = Time.time; // Aggiorna il timestamp dell'ultimo spawn
        hasSpawned = true; // Segna come spawnato
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            // Posizione casuale dentro il collider
            Vector2 spawnPos = new Vector2(
                Random.Range(transform.position.x - GetComponent<Collider2D>().bounds.extents.x,
                             transform.position.x + GetComponent<Collider2D>().bounds.extents.x),
                Random.Range(transform.position.y - GetComponent<Collider2D>().bounds.extents.y,
                             transform.position.y + GetComponent<Collider2D>().bounds.extents.y)
            );

            int enemyIndex = Random.Range(0, enemiesToSpawn.Length); // Sceglie casualmente un nemico dall'array
            Instantiate(enemiesToSpawn[enemyIndex], spawnPos, Quaternion.identity); // Istanzia il nemico nella scena
        }
    }
}
