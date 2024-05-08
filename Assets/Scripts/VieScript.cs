using System.Collections;
using UnityEngine;
 
public class VieScript : MonoBehaviour
{
    public GameObject Main1;
    public GameObject Main2;
    public float delai = 1f;

    void Start()
    {
        Main1.SetActive(false);
        Main2.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ActivationMains")
        {
            StartCoroutine(ActiverMains());
        }
    }

    IEnumerator ActiverMains()
    {
        yield return new WaitForSeconds(delai);
        Main1.SetActive(true);
        Main2.SetActive(true);

        GameObject activationMains = GameObject.Find("ActivationMains");

        if (activationMains != null)
        {
            Destroy(activationMains);
        }
    }

}

