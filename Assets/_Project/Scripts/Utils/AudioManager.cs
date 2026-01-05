using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip footstepsClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip levelUpClip;

    private AudioSource audioSource;
    private PlayerController playerController;
    [SerializeField] private float footstepInterval = 0.4f; // intervallo tra passi
    private float footstepTimer = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleFootsteps();
    }

    private void HandleFootsteps()
    {
        if (playerController.Direction != Vector2.zero)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f && footstepsClip != null)
            {
                audioSource.PlayOneShot(footstepsClip, 0.5f);
                footstepTimer = footstepInterval; // reset timer
            }
        }
        else
        {
            footstepTimer = 0f; // reset quando il player si derma
        }
    }

    public void PlayShoot()  // Da chiamare quando il player spara
    {
        if (shootClip != null)
            audioSource.PlayOneShot(shootClip);
    }

    
    public void PlayDamage() // da chiamare quando il player prende danno dai nemici
    {
         audioSource.PlayOneShot(damageClip);
    }

    public void PlayDeath() // Da chiamare quando il player muore
    {
        if (deathClip != null)
            audioSource.PlayOneShot(deathClip);
    }

    public void PlayPickup() // da chiamare quando il player prende l'arma da terra
    {
        if (pickupClip != null)
            audioSource.PlayOneShot(pickupClip);
    }

    public void PlayLevelUp() // da chiamare quando l'arma sale di livello
    {
        if (levelUpClip != null)
            audioSource.PlayOneShot(levelUpClip);
    }
}
