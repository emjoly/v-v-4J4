using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class FermerImageSlam : MonoBehaviour
{
    public GameObject Image;
    public PlayerMovement playerMovement;

    void Start()
    {
        playerMovement.enabled = false;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(DeactivateUI);
    }

    void DeactivateUI()
    {
        Image.SetActive(false);
        playerMovement.enabled = true;
    }
}