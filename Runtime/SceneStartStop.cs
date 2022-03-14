using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartStop : MonoBehaviour
{
    [SerializeField] string clip;
    private void Start()
    {
        GameObject audioManager = GameObject.Find("AudioManager");

        if (audioManager == null)
        {
            return;
        }

        audioManager.GetComponent<AudioManager>().TransitionIn(clip, 3f);
    }

    private void OnDestroy()
    {
        GameObject audioManager = GameObject.Find("AudioManager");

        if(audioManager == null)
        {
            return;
        }

        audioManager.GetComponent<AudioManager>().TransitionOut(clip, 3f);
    }
}
