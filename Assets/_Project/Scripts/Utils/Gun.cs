using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float shootDistance = 5f;
    [SerializeField] private int damage = 1;   // Danno base del bullet

    private int level = 1;                      // Livello iniziale dell'arma
    private float fireTimer;
    private LifeController playerLife; // riferimento al LifeController del Player
    private AudioManager playerAudio;   // riferimento al AudioManager del Player

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerLife = player.GetComponent<LifeController>();
            playerAudio = player.GetComponent<AudioManager>();
        }
    }

    private void Update()
    {
        if (playerLife != null && playerLife.IsDead) return; // Blocca sparo se morto

        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate && EnemyInRange()) // Se il timer ha raggiunto il limite e ci sono nemici in range, spara
        {
            Shoot();
            fireTimer = 0f; // reset del timer
        }
    }

    private void Shoot()
    {
        Vector2 mousePos = Input.mousePosition; // posizione del mouse sullo schermo
        Vector2 playerPos = new Vector2(Screen.width / 2, Screen.height / 2); // centro dello schermo = player fisso
        Vector2 direction = (mousePos - playerPos).normalized;

        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.SetDirection(direction);
            bullet.SetDamage(damage); // imposta il danno in base al livello
        }

        if (playerAudio != null)
        {
            playerAudio.PlayShoot();
        }
    }


    private bool EnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position,enemy.transform.position);

            if (distance <= shootDistance)
            {
                return true;
            }
        }
        
        return false;
    }

    public void LevelUp()
    {
        level++;
        damage += 1;        // aumenta il danno per livello
        fireRate += 0.5f;   // aumenta il fire rate
        Debug.Log($"Gun leveled up! Level {level} | Damage {damage} | Fire Rate {fireRate}");

        AudioManager audioManager = FindObjectOfType<AudioManager>(); // audio per il levelup
        if (audioManager != null)
        {
            audioManager.PlayLevelUp();
        }
    }
}


