using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    List<AudioSource>   audioSources;

    public static SoundManager instance;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        audioSources = new List<AudioSource>();
        GetComponentsInChildren<AudioSource>(true, audioSources);
    }

    void _PlaySound(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.volume = volume;
                audioSource.pitch = pitch;
                audioSource.Play();

                return;
            }
        }
        GameObject newAudioSourceObject = new GameObject();
        newAudioSourceObject.name = "AudioSource";
        newAudioSourceObject.transform.parent = transform;
        AudioSource newAudioSource = newAudioSourceObject.AddComponent<AudioSource>();
        newAudioSource.clip = clip;
        newAudioSource.volume = volume;
        newAudioSource.pitch = pitch;
        newAudioSource.Play();

        audioSources.Add(newAudioSource);
    }

    public static void PlaySound(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        instance._PlaySound(clip, volume, pitch);
    }
}
