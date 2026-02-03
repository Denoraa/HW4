using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip scoreClip;
    public void PlaySFX(bool isJump)
    {
        if (isJump)
        {
            sfxSource.PlayOneShot(jumpClip);
            return;
        }

        sfxSource.PlayOneShot(scoreClip);

    }

  
}