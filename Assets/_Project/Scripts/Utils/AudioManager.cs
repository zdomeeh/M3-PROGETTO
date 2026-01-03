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
            footstepTimer = 0f; // reset quando ti fermi
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
