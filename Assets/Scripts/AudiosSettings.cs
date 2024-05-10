using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosSettings : MonoBehaviour
{
public static AudioClip sonHit, sonAttack, sonJump, sonMarche;
static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        sonHit = Resources.Load<AudioClip>("AttackHitTest");
        sonAttack = Resources.Load<AudioClip>("AttackTest");
        sonJump = Resources.Load<AudioClip>("SautTest");
        sonMarche = Resources.Load<AudioClip>("MarcheTest");

        audioSrc = GetComponent<AudioSource>();

            if (audioSrc == null)
    {
        audioSrc = gameObject.AddComponent<AudioSource>();
    }
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "AttackHitTest":
                audioSrc.PlayOneShot(sonHit);
                break;
            case "AttackTest":
                audioSrc.PlayOneShot(sonAttack);
                break;
            case "SautTest":
                audioSrc.PlayOneShot(sonJump);
                break;
            case "MarcheTest":
                audioSrc.PlayOneShot(sonMarche);
                break;
        }
    }
}
