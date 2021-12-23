using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;

public class MusicPlayerScript : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    private Handedness handedness = Handedness.Left;
    private bool nextFlag = false;
    private int nextCool = 0;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.pitch = 1.0f;
        nextFlag = false;
        nextCool = 0;
    }

    void Update()
    {
        if (nextFlag)
        {
            nextCool++;
            if (nextCool == 60)
            {
                _NextAudio();
                nextCool = 0;
            }
        } else
        {
            nextCool = 0;
        }
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void _Play() // left thumbs up
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }

    public void _Stop() // left hand
    {
        audioSource.Stop();
    }

    public void _Pause()
    {
        audioSource.Pause();
    }

    public void _UnPause()
    {
        audioSource.UnPause();
    }

    public void _NextAudio() // left fist
    {
        audioSource.Stop();
        audioSource.clip = GetRandomClip();
        audioSource.Play();
    }

    public void _IncreaseVolume() // left index
    {
        if (audioSource.volume <= 0.7f)
            audioSource.volume = audioSource.volume + 0.2f;
    }

    public void _DecreaseVolume() // left two
    {
        if (audioSource.volume >= 0.3f)
            audioSource.volume = audioSource.volume - 0.2f;
    }

    public void _IncreaseSpeed() // left three
    {
        if (audioSource.pitch >= 1.75f)
        {
            audioSource.pitch = 1.75f;
        }
        else
        {
            audioSource.pitch = audioSource.pitch + 0.25f;
        }
    }

    public void _DecreaseSpeed() // left four
    {
        if (audioSource.pitch <= 0.75f)
        {
            audioSource.pitch = 0.75f;
        }
        else
        {
            audioSource.pitch = audioSource.pitch - 0.25f;
        }
    }

    public void setSpeed(float run_speed)
    {
        audioSource.pitch = (run_speed - 0.06f) / 0.24f + 0.75f;
    }

    public void setNextflag(bool flag)
    {
        nextFlag = flag;

    }
}

