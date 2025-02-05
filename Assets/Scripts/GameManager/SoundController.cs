using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;
    public static SoundController Instance { get { return instance; } }

    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource audioSource;

    [Header("---------Audio Clip---------")]
    public AudioClip jump;
    public AudioClip land;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
