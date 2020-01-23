using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAudio : MonoBehaviour
{
    public AudioClip Spawn;
    public AudioClip Bounce;
    public AudioClip Death;
    public AudioClip Clink;

    public AudioSource audioSource;

    public void PlaySpawnSound()
    {
        audioSource.clip = Spawn;
        audioSource.Play();
    }

    public void PlayBounceSound()
    {
        audioSource.clip = Bounce;
        audioSource.Play();
    }

    public void PlayClinkSound()
    {
        audioSource.clip = Clink;
        audioSource.Play();
    }

    public void PlayDeathSound()
    {
        audioSource.clip = Death;
        audioSource.Play();
    }
}
