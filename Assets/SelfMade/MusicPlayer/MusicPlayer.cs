using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    private Handedness handedness = Handedness.Left;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    void Update()
    {
        if (HandPoseUtils.IsThumbGrabbing(handedness))
        {
            if (!audioSource.isPlaying) {
                audioSource.clip = GetRandomClip();
                audioSource.Play();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            audioSource.Stop();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            audioSource.UnPause();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            NextAudio();
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            audioSource.volume = audioSource.volume + 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            audioSource.volume = audioSource.volume - 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            audioSource.pitch = audioSource.pitch + 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            audioSource.pitch = audioSource.pitch - 0.1f;
        }
    }

    private void NextAudio()
    {
        audioSource.Stop();
        audioSource.clip = GetRandomClip();
        audioSource.Play();
    }
}
