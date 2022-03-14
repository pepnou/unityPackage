using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] float volume = 1f;
    [SerializeField] Sound[] sounds;

    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.SetAudioSource(gameObject.AddComponent<AudioSource>());
            s.SetVolumeMult(volume);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.Stop();
    }

    public void TransitionIn(string name, float time)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        StartCoroutine(s.TransitionIn(time));
    }

    public void TransitionOut(string name, float time)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        StartCoroutine(s.TransitionOut(time));
    }

    public void SetVolume(float _volume)
    {
        volume = Mathf.Clamp(_volume, 0f, 1f);

        foreach (Sound s in sounds)
        {
            s.SetVolumeMult(volume);
        }
    }




    [System.Serializable]
    class Sound
    {
        [SerializeField] string _name;
        [SerializeField] AudioClip _clip;
        [Range(0f, 1f)] [SerializeField] float _volume = 1f;
        [Range(0.1f, 3f)] [SerializeField] float _pitch = 1f;
        [SerializeField] bool _loop = false;

        public string name { get => _name; }
        public AudioClip clip { get => _clip; }
        public float volume { get => _volume; }
        public float pitch { get => _pitch; }
        public bool loop { get => _loop; }

        private float volumeMult = 1f;
        AudioSource source;

        public void SetVolumeMult(float _volumeMult)
        {
            volumeMult = Mathf.Clamp(_volumeMult, 0f, 1f);
            source.volume = volume * volumeMult;
        }

        public void SetAudioSource(AudioSource _source)
        {
            source = _source;
            source.clip = clip;
            source.volume = volume * volumeMult;
            source.pitch = pitch;
            source.loop = loop;
        }
        public void Play()
        {
            source.Play();
        }
        public void Stop()
        {
            source.Stop();
        }
        public IEnumerator TransitionIn(float time)
        {
            if(!source.isPlaying)
            {
                source.volume = 0f;
                Play();
            }

            float startTime = Time.realtimeSinceStartup;
            float startVolume = source.volume;

            while (Time.realtimeSinceStartup - startTime < time)
            {
                source.volume = Mathf.Lerp(startVolume, volume, (Time.realtimeSinceStartup - startTime) / time);
                yield return null;
            }

            source.volume = volume;
        }
        public IEnumerator TransitionOut(float time)
        {
            if (!source.isPlaying)
            {
                yield break;
            }

            float startTime = Time.realtimeSinceStartup;
            float startVolume = source.volume;

            while (Time.realtimeSinceStartup - startTime < time)
            {
                source.volume = Mathf.Lerp(startVolume, 0f, (Time.realtimeSinceStartup - startTime) / time);
                yield return null;
            }

            source.volume = 0f;
            Stop();
        }

        public bool isPlaying()
        {
            return source.isPlaying;
        }
    }
}
