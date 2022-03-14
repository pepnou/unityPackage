using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    AudioManager audioManager = null;

    public void SetVolume(float volume)
    {
        if(audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        audioManager.SetVolume(volume);
    }
}
