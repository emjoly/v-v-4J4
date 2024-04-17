using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // On déclare un Slider pour le volume
    public Slider volumeSlider;

    public void Start()
    {
        // On charge le volume
        Load();
    }
    // Fonction pour changer le volume
    public void ChangeVolume()
    {   // On change le volume de l'AudioListener
        AudioListener.volume = volumeSlider.value;
        // On appel la fonction 
        Save();
    }
    // Fonction pour charger le volume
    private void Load()
    {   // On vérifie si la clé existe
        if (PlayerPrefs.HasKey("volumeLevel"))
        {
            // On récupère la valeur du volume
            volumeSlider.value = PlayerPrefs.GetFloat("volumeLevel");
        }
        else
        {
            // Volume par défaut 
            volumeSlider.value = 1f;
            // On appel la fonction Save pour sauvegarder le volume par défaut
            Save();
        }
    }
    // Fonction pour sauvegarder le volume
    private void Save()
    {
        // On sauvegarde le volume
        PlayerPrefs.SetFloat("volumeLevel", volumeSlider.value);
    }
}
