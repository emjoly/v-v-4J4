using System.Collections;
using UnityEngine;

public class SpikesAndBeam : MonoBehaviour
{
    public GameObject[] spikeObjects;
    public GameObject[] beamObjects;

    private float spikeTimer = 0;
    private float beamTimer = 0;
    private float spikeActivationInterval = 5f;
    private float beamActivationInterval = 10f;

    void Update()
    {
        spikeTimer += Time.deltaTime;
        beamTimer += Time.deltaTime;

        if (spikeTimer > spikeActivationInterval)
        {
            spikeTimer = 0;

            foreach (GameObject spike in spikeObjects)
            {
                spike.SetActive(true);
            }

            StartCoroutine(DeactivateAfterSeconds(spikeObjects));
        }

        if (beamTimer > beamActivationInterval)
        {
            beamTimer = 0;

            foreach (GameObject beam in beamObjects)
            {
                beam.SetActive(true);
            }

            StartCoroutine(DeactivateAfterSeconds(beamObjects));
        }
    }

    IEnumerator DeactivateAfterSeconds(GameObject[] objects)
    {
        Animator animator = objects[0].GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float seconds = stateInfo.length;

        yield return new WaitForSeconds(seconds);
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
