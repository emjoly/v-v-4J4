using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour
{
    // Slider pour la barre de vie
    public Slider slider;
        // Fonction pour définir la vie max
        public void SetMaxHealth(int health)
    {
        // On définit la valeur max de la barre de vie
        slider.maxValue = health;
        // On définit la valeur actuelle de la barre de vie
        slider.value = health;
    }
    // Fonction pour définir la vie actuelle
    public void SetHealth(int health)
    {
        // On définit la valeur actuelle de la barre de vie
        slider.value = health;
    }

}
