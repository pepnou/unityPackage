using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;

    [SerializeField] [Range(0f, 1f)] private float _volume = 1f;
    [SerializeField] [Range(0.1f, 3f)] private float _pitch = 1f;
    [SerializeField] private bool _loop = false;

    [SerializeField] private string groupName;

    private AudioSource _source;

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
        //if (!_source.isPlaying)
        //{
        //    _source.volume = 0f;
        //    Play();
        //}
        if (_source.isPlaying)
            yield break;
        _source.volume = 0f;
        Play();

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
            yield break;

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
        Debug.Log("TransitionToward");
        Debug.Log(_source);
        float startTime = Time.realtimeSinceStartup;

        float startVolume = _source.volume;
        float startPitch = _source.pitch;

        while (Time.realtimeSinceStartup - startTime < time)
        {
            _volume = Mathf.Lerp(startVolume, s.volume * volumeMult, (Time.realtimeSinceStartup - startTime) / time);
            _pitch = Mathf.Lerp(startPitch, s.pitch, (Time.realtimeSinceStartup - startTime) / time);

            _source.volume = _volume;
            _source.pitch = _pitch;

            yield return null;
        }

        _volume = s.volume * volumeMult;
        _pitch = s.pitch;
        _loop = s._loop;

        _source.volume = _volume;
        _source.pitch = _pitch;
        _source.loop = _loop;
    }

    public bool isPlaying()
    {
        return _source.isPlaying;
    }



    public static bool operator ==(Sound s1, Sound s2)
    {
        if (s1 is null && s2 is null)
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