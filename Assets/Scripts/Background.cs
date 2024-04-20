using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Awake est appelé lorsque le script est chargé
    void Awake()
    {
        // Si une autre instance de Background existe déjà, on la détruit
        if (FindObjectsOfType<Background>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
}

