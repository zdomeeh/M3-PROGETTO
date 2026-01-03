using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesToSpawn; // Prefab nemici da spawnare
    [SerializeField] private int numberToSpawn = 3;       // Quanti nemici spawnare
    [SerializeField] private bool spawnOnce = true;       // Spawn solo una volta
    [SerializeField] private float respawnCooldown = 5f;  // Tempo in secondi tra spawn multipli

    private bool hasSpawned = false;
    private float lastSpawnTime = -Mathf.Infinity; // Timestamp ultimo spawn

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
        // Controlla cooldown e spawnOnce
        if (spawnOnce && hasSpawned) return;
        if (Time.time < lastSpawnTime + respawnCooldown) return;

        SpawnEnemies();
        lastSpawnTime = Time.time;
        hasSpawned = true;
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

            int enemyIndex = Random.Range(0, enemiesToSpawn.Length);
            Instantiate(enemiesToSpawn[enemyIndex], spawnPos, Quaternion.identity);
        }
    }
}
