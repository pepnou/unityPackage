using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] protected float globalVolume = 1f;
    [SerializeField] float destroySpeed = 2f;

    [SerializeField] protected List<Sound> soundsBank;

    /*[SerializeField] SoundGroup[] volumeGroups;
    [SerializeField] SoundGroup[] soundGroups;*/

    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            instance.SceneTransition(this);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in soundsBank)
        {
            s.SetAudioSource(gameObject.AddComponent<AudioSource>());
            s.SetVolumeMult(globalVolume);
        }
    }


    protected void SceneTransition(AudioManager newManager)
    {


        /*
        for (int i = soundsBank.Count - 1; i >= 0; i--)
        {
            Sound s = soundsBank[i];
            if (s.isPlaying())
            {
                s.name = s.name + "-BEING-DESTROYED";
                StartCoroutine(s.TransitionOutAndDestroy(destroySpeed));
            }
            soundsBank.Remove(s);
        }

        globalVolume = newManager.globalVolume;
        destroySpeed = newManager.destroySpeed;

        foreach (Sound s in newManager.soundsBank)
        {
            Debug.Log(s.name);
            if(!Contains(s))
            {
                Sound newSound = new Sound(s);
                soundsBank.Add(newSound);
                newSound.SetAudioSource(gameObject.AddComponent<AudioSource>());
                newSound.SetVolumeMult(globalVolume);
            }
        }
        */


        for (int i = soundsBank.Count - 1; i >= 0; i--)
        {
            Sound s = soundsBank[i];
            Sound s2 = newManager.soundsBank.Find(finder => finder == s);

            if(s2 != null)
            {
                StartCoroutine(s.TransitionToward(s2, destroySpeed));
                newManager.soundsBank.Remove(s2);
            } else
            {
                StartCoroutine(TransitionOutAndDestroy(s, destroySpeed));
                soundsBank.Remove(s);
            }
        }

        //globalVolume = newManager.globalVolume;
        destroySpeed = newManager.destroySpeed;

        foreach (Sound s in newManager.soundsBank)
        {
            Sound newSound = new Sound(s);
            soundsBank.Add(newSound);

            newSound.SetAudioSource(gameObject.AddComponent<AudioSource>());
            newSound.SetVolumeMult(globalVolume);
        }
    }

    IEnumerator TransitionOutAndDestroy(Sound s, float time)
    {
        yield return s.TransitionOut(time);
        Destroy(s.source);
    }











    public void Play(string name)
    {
        Sound s = soundsBank.Find(sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.Play();
    }

    public void Stop(string name)
    {
        Sound s = soundsBank.Find(sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.Stop();
    }

    public void TransitionIn(string name, float time)
    {
        Sound s = soundsBank.Find(sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        StartCoroutine(s.TransitionIn(time));
    }

    public void TransitionOut(string name, float time)
    {
        Sound s = soundsBank.Find(sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        StartCoroutine(s.TransitionOut(time));
    }

    public void SetVolume(float _volume)
    {
        globalVolume = Mathf.Clamp(_volume, 0f, 1f);

        foreach (Sound s in soundsBank)
        {
            s.SetVolumeMult(globalVolume);
        }
    }

    public float GetVolume()
    {
        return globalVolume;
    }





    [System.Serializable]
    protected class Sound
    {
        [SerializeField] string _name;
        [SerializeField] AudioClip _clip;
        [SerializeField] [Range(0f, 1f)] float _volume = 1f;
        [SerializeField] [Range(0.1f, 3f)] float _pitch = 1f;
        [SerializeField] bool _loop = false;
        AudioSource _source;
        float volumeMult = 1f;

        public string name { get => _name; set => _name = value; }
        public AudioClip clip { get => _clip; }
        public float volume { get => _volume; }
        public float pitch { get => _pitch; }
        public bool loop { get => _loop; }

        public AudioSource source { get => _source; }




        public Sound(string name, AudioClip clip, float volume, float pitch, bool loop)
        {
            _name = name;
            _clip = clip;
            _volume = volume;
            _pitch = pitch;
            _loop = loop;
        }
        public Sound(Sound s)
        {
            _name = s.name;
            _clip = s.clip;
            _volume = s.volume;
            _pitch = s.pitch;
            _loop = s.loop;
        }



        public void SetVolumeMult(float _volumeMult)
        {
            volumeMult = Mathf.Clamp(_volumeMult, 0f, 1f);
            _source.volume = volume * volumeMult;
        }

        public void SetAudioSource(AudioSource _source)
        {
            this._source = _source;
            this._source.clip = clip;
            this._source.volume = volume * volumeMult;
            this._source.pitch = pitch;
            this._source.loop = loop;
        }
        public void Play()
        {
            _source.Play();
        }
        public void Stop()
        {
            _source.Stop();
        }
        public IEnumerator TransitionIn(float time)
        {
            if (!_source.isPlaying)
            {
                _source.volume = 0f;
                Play();
            }

            float startTime = Time.realtimeSinceStartup;

            float startVolume = _source.volume;
            float targetVolume = volume * volumeMult;

            while (Time.realtimeSinceStartup - startTime < time)
            {
                _source.volume = Mathf.Lerp(startVolume, targetVolume, (Time.realtimeSinceStartup - startTime) / time);
                yield return null;
            }

            _source.volume = targetVolume;
        }
        public IEnumerator TransitionOut(float time)
        {
            if (!_source.isPlaying)
            {
                yield break;
            }

            float startTime = Time.realtimeSinceStartup;
            float startVolume = _source.volume;

            while (Time.realtimeSinceStartup - startTime < time)
            {
                _source.volume = Mathf.Lerp(startVolume, 0f, (Time.realtimeSinceStartup - startTime) / time);
                yield return null;
            }

            _source.volume = 0f;
            Stop();
        }

        public IEnumerator TransitionToward(Sound s, float time)
        {
            float startTime = Time.realtimeSinceStartup;

            float startVolume = source.volume;
            float startPitch  = source.pitch;

            while (Time.realtimeSinceStartup - startTime < time)
            {
                _volume = Mathf.Lerp(startVolume, s.volume * volumeMult, (Time.realtimeSinceStartup - startTime) / time);
                _pitch  = Mathf.Lerp(startPitch , s.pitch, (Time.realtimeSinceStartup - startTime) / time);

                _source.volume = _volume;
                _source.pitch  = _pitch;
                
                yield return null;
            }

            _volume = s.volume * volumeMult;
            _pitch  = s.pitch;
            _loop   = s._loop;

            _source.volume = _volume;
            _source.pitch  = _pitch;
            _source.loop   = _loop;
        }

        public bool isPlaying()
        {
            return _source.isPlaying;
        }

        

        public static bool operator ==(Sound s1, Sound s2)
        {
            if(s1 is null && s2 is null)
                return true;
            if (s1 is null || s2 is null)
                return false;

            return s1.name == s2.name && s1.clip == s2.clip;
        }

        public static bool operator !=(Sound s1, Sound s2)
        {
            return !(s1 == s2);
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Sound))
            {
                return false;
            }

            Sound s = (Sound)obj;
            return (this == s);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}