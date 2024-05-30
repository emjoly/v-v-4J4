using System.Collections;
using UnityEngine;

public class SpikesAndBeam : MonoBehaviour
{
    public GameObject[] spikeObjects;
    public GameObject[] beamObjects;

    private float timer = 0;
    private float cycleDuration = 15f;

    void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2 && timer < 3.5)
        {
            foreach (GameObject spike in spikeObjects)
            {
                spike.SetActive(true);
            }
        }
        else if (timer >= 3.5 && timer < 5)
        {
            foreach (GameObject spike in spikeObjects)
            {
                spike.SetActive(false);
            }
        }
        else if (timer > 4.5 && timer < 6.5)
        {
            foreach (GameObject beam in beamObjects)
            {
                beam.SetActive(true);
            }
        }
        else if (timer >= 6.5 && timer < 7.5)
        {
            foreach (GameObject beam in beamObjects)
            {
                beam.SetActive(false);
            }
        }
        else if (timer >= 7.5 && timer < 9.5)
        {
            spikeObjects[0].SetActive(true);
            spikeObjects[1].SetActive(true);
            spikeObjects[5].SetActive(true);
            spikeObjects[6].SetActive(true);
            beamObjects[2].SetActive(true);
            beamObjects[3].SetActive(true);
        }

        else if (timer >= 10.5 && timer < 12)
        {
            spikeObjects[2].SetActive(true);
            spikeObjects[3].SetActive(true);
            spikeObjects[4].SetActive(true);
            beamObjects[0].SetActive(true);
            beamObjects[1].SetActive(true);
            beamObjects[4].SetActive(true);
            beamObjects[5].SetActive(true);
        }

        else if (timer >= 13 && timer < 15)
        {
            foreach (GameObject spike in spikeObjects)
            {
                spike.SetActive(true);
            }
            foreach (GameObject beam in beamObjects)
            {
                beam.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject spike in spikeObjects)
            {
                spike.SetActive(false);
            }
            foreach (GameObject beam in beamObjects)
            {
                beam.SetActive(false);
            }
        }

        if (timer >= cycleDuration)
        {
            timer = 0;
        }
    }
}
