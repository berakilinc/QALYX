using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        PlayRandomTrack();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomTrack();
        }
    }

    void PlayRandomTrack()
    {
        if (musicTracks.Length == 0) return;

        int randomID = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[randomID];
        audioSource.Play();
    }
}