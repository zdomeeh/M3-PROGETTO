using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab; // Prefab dell'arma da droppare
    [SerializeField][Range(0, 100)] private int dropChance = 10; // Probabilità in percentuale

    // Chiamato dal nemico quando muore
    public void TryDrop()
    {
        int roll = Random.Range(0, 100); // Genera un numero casuale tra 0 e 99
        if (roll < dropChance) // Confronta con la probabilità di drop
        {
            Vector3 dropPos = transform.position + Vector3.up * 0.5f; // Calcola la posizione del drop leggermente sopra il nemico
            Instantiate(weaponPrefab, dropPos, Quaternion.identity); // Istanzio l'arma nella scena
        }
    }
}