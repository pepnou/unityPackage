using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;


[ExecuteInEditMode]
public class AudioManager2 : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] protected float globalVolume = 1f;
    [SerializeField] float destroySpeed = 2f;

    [SerializeField] protected List<Sound> soundsBank;

    [Serializable]
    public struct SoundTransitionInfo
    {
        public string clip;
        public float time;
    }
    [SerializeField] protected List<SoundTransitionInfo> soundsToStart;
    [SerializeField] protected List<SoundTransitionInfo> soundsToStop;

    [SerializeField] Group MasterGroup;
    private List<string> groupNames = null;
    Dictionary<string, Group> groupDict = null;

    private static AudioManager2 instance;
    private void Awake()
    {


        if (instance == null)
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
        
        foreach (SoundTransitionInfo soundTransitionInfo in soundsToStart)
        {
            TransitionIn(soundTransitionInfo.clip, soundTransitionInfo.time);
        }
    }

















    /*public List<string> GetGroupNames_internal()
    {
        if (groupNames == null || groupNames.Count == 0)
        {
            UpdateGroupNames_internal();
        }

        return groupNames;
    }

    public static List<string> GetGroupNames()
    {
        return instance.GetGroupNames_internal();
    }

    public void UpdateGroupNames_internal()
    {
        if(groupNames == null)
            groupNames = new List<string>();

        groupNames.Clear();
        MasterGroup.GetGroupName(groupNames);
    }

    public static void UpdateGroupNames()
    {
        instance.UpdateGroupNames_internal();
    }

    public Group FindGroup(string name)
    {
        return groupDict.GetValueOrDefault(name);
    }*/




    public void BuildGroupDictionary()
    {
        if (groupDict == null || groupDict.Count == 0)
            groupDict = new Dictionary<string, Group>();

        UpdateGroupDictionnary();
    }

    public void UpdateGroupDictionnary()
    {
        groupDict.Clear();
        MasterGroup.BuildGroupDictionary(groupDict);
    }

    public List<string> GetGroupNames_internal()
    {
        if (groupDict == null || groupDict.Count == 0)
            BuildGroupDictionary();

        return new List<string>(groupDict.Keys);
    }



















    protected void SceneTransition(AudioManager2 newManager)
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


        foreach (SoundTransitionInfo soundTransitionInfo in soundsToStop)
        {
            Sound sound = soundsBank.Find(finder => finder.name == soundTransitionInfo.clip);
            if (sound == null)
            {
                Debug.LogError("Sounds " + soundTransitionInfo.clip + " wasn't fount in AudioManager audio bank.");
                continue;
            }
            sound.name += "_BEING_STOPED";
            StartCoroutine(TransitionOutAndDestroy(sound, soundTransitionInfo.time));
            soundsBank.Remove(sound);
        }


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

        foreach (SoundTransitionInfo soundTransitionInfo in soundsToStart)
        {
            TransitionIn(soundTransitionInfo.clip, soundTransitionInfo.time);
        }
    }

    IEnumerator TransitionOutAndDestroy(Sound s, float time)
    {
        yield return s.TransitionOut(time);
        Destroy(s.source);
    }











    private void Play_internal(string name)
    {
        Sound s = soundsBank.Find(sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.Play();
    }
    public static void Play(string name)
    {
        instance.Play_internal(name);
    }


    private void Stop_internal(string name)
    {
        Sound s = soundsBank.Find(sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.Stop();
    }
    public static void Stop(string name)
    {
        instance.Stop_internal(name);
    }


    private void TransitionIn_internal(string name, float time)
    {
        Sound s = soundsBank.Find(sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        StartCoroutine(s.TransitionIn(time));
    }
    public static void TransitionIn(string name, float time)
    {
        instance.TransitionIn_internal(name, time);
    }


    private void TransitionOut_internal(string name, float time)
    {
        Sound s = soundsBank.Find(sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        StartCoroutine(s.TransitionOut(time));
    }
    public static void TransitionOut(string name, float time)
    {
        instance.TransitionOut_internal(name, time);
    }


    private void SetVolume_internal(float _volume)
    {
        globalVolume = Mathf.Clamp(_volume, 0f, 1f);

        foreach (Sound s in soundsBank)
        {
            s.SetVolumeMult(globalVolume);
        }
    }
    public static void SetVolume(float _volume)
    {
        instance.SetVolume_internal(_volume);
    }


    private float GetVolume_internal()
    {
        return globalVolume;
    }
    public static float GetVolume()
    {
        return instance.GetVolume_internal();
    }
}

