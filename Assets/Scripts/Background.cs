using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // variable pour la musique de fond
    private static Background backgroundMusic;
    // Awake est appelé lorsque le script est chargé
    void Awake()
    {
        // Si backgroundMusic est null
        if (backgroundMusic == null)
        {
            // On le défini comme étant le script
            backgroundMusic = this;
        }
        else
        {
            // Sinon on le détruit
            Destroy(gameObject);
        }
    }
}
