using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject gunPrefab; // Prefab dell'arma da raccogliere

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Controlla se il Player entra in contatto con il pickup
        {
            GameObject gunInstance = Instantiate(gunPrefab, collision.transform); // Istanzia l'arma come figlio del Player

            gunInstance.transform.localPosition = Vector3.zero; // Posiziona l'arma sul player (ad esempio al centro)

            gunInstance.transform.localRotation = Quaternion.identity; // La uso per ruotare correttamente la Gun 

            Destroy(gameObject); // Distrugge il pickup dalla scena
        }
    }
}
