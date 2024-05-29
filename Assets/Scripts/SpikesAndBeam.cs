using System.Collections;
using UnityEngine;

public class SpikesAndBeam : MonoBehaviour
{
    public GameObject[] spikeObjects;
    public GameObject[] beamObjects;

    private float timer = 0;
    private float cycleDuration = 15f; // Total duration of one cycle

    void OnEnable()
    {
        // Reset the timer whenever the GameObject is activated
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Previous behavior (2 to 3.5 seconds)
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
        // Previous behavior (5 to 6.5 seconds)
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
            spikeObjects[0].SetActive(true); // Activate spike 1
            spikeObjects[1].SetActive(true); // Activate spike 2
            spikeObjects[5].SetActive(true); // Activate spike 3
            spikeObjects[6].SetActive(true); // Activate spike 4
            beamObjects[2].SetActive(true); // Activate beam 3
            beamObjects[3].SetActive(true); // Activate beam 4
        }

        else if (timer >= 10.5 && timer < 12)
        {
            spikeObjects[2].SetActive(true); // Activate spike 5
            spikeObjects[3].SetActive(true); // Activate spike 6
            spikeObjects[4].SetActive(true); // Activate spike 7
            beamObjects[0].SetActive(true); // Activate beam 1
            beamObjects[1].SetActive(true); // Activate beam 2
            beamObjects[4].SetActive(true); // Activate beam 5
            beamObjects[5].SetActive(true); // Activate beam 6
        }
        // New behavior (13 to 15 seconds)
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
            // Deactivate all spikes and beams
            foreach (GameObject spike in spikeObjects)
            {
                spike.SetActive(false);
            }
            foreach (GameObject beam in beamObjects)
            {
                beam.SetActive(false);
            }
        }

        // Reset timer and loop
        if (timer >= cycleDuration)
        {
            timer = 0;
        }
    }
}
