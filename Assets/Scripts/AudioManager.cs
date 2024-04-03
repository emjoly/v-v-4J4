using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;

    public void Start()
    {
        Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("volumeLevel"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volumeLevel");
        }
        else
        {
            // Set a default volume level
            volumeSlider.value = 1f; // For example, you can set it to maximum volume
            Save(); // Save the default value
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volumeLevel", volumeSlider.value);
    }
}
