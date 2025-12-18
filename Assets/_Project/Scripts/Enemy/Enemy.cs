using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;       // Velocità del nemico
    [SerializeField] private int damageToPlayer = 1; // Danno inflitto al Player

    private Transform player;
    private LifeController playerLife;

    private void Awake()
    {
        // Trova il Player nella scena
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerLife = playerObj.GetComponent<LifeController>();
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Movimento verso il Player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se collide con il Player, infligge danno
        if (collision.gameObject.CompareTag("Player") && playerLife != null)
        {
            playerLife.TakeDamage(damageToPlayer);
            // Distrugge se vuoi che il nemico sparisca dopo il contatto:
            Destroy(gameObject);
        }
    }
}
