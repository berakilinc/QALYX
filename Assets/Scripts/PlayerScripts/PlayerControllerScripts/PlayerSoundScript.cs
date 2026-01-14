using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip walkSound;

    public PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (playerController.isPlayerMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkSound;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
