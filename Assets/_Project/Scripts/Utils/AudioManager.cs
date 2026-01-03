using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip footstepsClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip damageClip;

    private AudioSource audioSource;
    private PlayerController playerController;

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
        // Suono dei passi solo se il player si muove
        if (playerController.Direction != Vector2.zero)
        {
            if (!audioSource.isPlaying && footstepsClip != null)
            {
                audioSource.PlayOneShot(footstepsClip, 0.5f); // volume 0.5
            }
        }
    }

    // Da chiamare quando il player spara
    public void PlayShoot()
    {
        if (shootClip != null)
            audioSource.PlayOneShot(shootClip);
    }

    
    public void PlayDamage()
    {
         audioSource.PlayOneShot(damageClip);
    }

    // Da chiamare quando il player muore
    public void PlayDeath()
    {
        if (deathClip != null)
            audioSource.PlayOneShot(deathClip);
    }
}
