using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixer mixer;

    public void VolGeral(float sliderValue)
    {
        mixer.SetFloat("VolMaster", Mathf.Log10(sliderValue) *20);
    }

    public void VolWave(float sliderValue)
    {
        mixer.SetFloat("VolWave", Mathf.Log10(sliderValue) * 20);
    }

    public void VolTiro(float sliderValue)
    {
        mixer.SetFloat("VolTiro", Mathf.Log10(sliderValue) * 20);
    }
}
