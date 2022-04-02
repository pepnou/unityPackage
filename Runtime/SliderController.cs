using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private void OnValidate()
    {
        if(!TryGetComponent<Slider>(out Slider tmp))
        {
            Debug.LogError("SliderController missing slider component");
        }
    }
    private void Start()
    {
        GetComponent<Slider>().value = AudioManager.instance.GetVolume();
    }

    public void SetVolume(float volume)
    {
        AudioManager.instance.SetVolume(volume);
    }
}
