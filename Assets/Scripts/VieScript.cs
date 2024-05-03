using System.Collections;
using UnityEngine;

public class VieScript : MonoBehaviour
{
    public GameObject sideHand;
    public GameObject sideHand2;

    void OnEnable()
    {
        ActiverSideHands();
    }

    public void ActiverSideHands()
    {
        sideHand.SetActive(true);
        sideHand2.SetActive(true);
    }
}

