using System;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip buttonHoverSound;

    void Start()
    {
            audioSource = GetComponent<AudioSource>();
    }

    public void ButtonClickSoundPlay()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void ButtonHoverSoundPlay()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(buttonHoverSound);
        }
    }
}
