using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject gunPrefab; // Prefab dell'arma da raccogliere

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Gun existingGun = collision.GetComponentInChildren<Gun>(); // Controlla se il player ha già un'arma

            if (existingGun != null)
            {
                // Livella l'arma esistente
                existingGun.LevelUp();
            }
            else
            {
                // Istanzia l'arma se il player non la possiede
                GameObject gunInstance = Instantiate(gunPrefab, collision.transform);
                gunInstance.transform.localPosition = Vector3.zero;
                gunInstance.transform.localRotation = Quaternion.identity;
            }

            // Chiama l'AudioManager del player
            AudioManager playerAudio = collision.GetComponent<AudioManager>();
            if (playerAudio != null)
            {
                playerAudio.PlayPickup();
            }

            Destroy(gameObject); // Rimuove il pickup dalla scena
        }
    }
}
