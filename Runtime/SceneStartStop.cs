using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartStop : MonoBehaviour
{
    [SerializeField] string clip;
    [SerializeField] float duration = 3f;
    private void Start()
    {
        AudioManager2.TransitionIn(clip, duration);
        //AudioManager.instance.Play(clip);
    }
    
    /*private void OnDestroy()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.TransitionOut(clip, duration);
        }
    }*/
}
