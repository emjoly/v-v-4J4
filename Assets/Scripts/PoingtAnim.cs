using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoingtAnim : MonoBehaviour
{
    public float speed = 10.0f;
    public Transform player;
    public Boss boss;
    public bool isMoving = false;
    private bool isFirstTime = true;
    public GameObject vieObject;
    public GameObject LumiereVie;
    public GameObject LumiereVie2;
    public GameObject ActivationMains;
    private Coroutine fistRoutine;
    public bool bossIsDead = false;  // Add this flag

    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 10, transform.position.z);
        fistRoutine = StartCoroutine(FistRoutine());
    }

    void Update()
    {
        if (isMoving && transform.position.y < player.position.y)
        {
            transform.position = new Vector3(player.position.x, player.position.y + 10, transform.position.z);
        }

        if (isMoving)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    public void StopFistRoutine()
    {
        if (fistRoutine != null)
        {
            StopCoroutine(fistRoutine);
            isMoving = false;
        }
    }

    IEnumerator FistRoutine()
    {
        while (!bossIsDead)  // Check the flag here
        {
            if (isFirstTime)
            {
                yield return new WaitForSeconds(2);
                isFirstTime = false;
            }

            isMoving = true;
            yield return new WaitForSeconds(10);

            isMoving = false;
            yield return new WaitForSeconds(5);

            isMoving = true;
        }

        if (bossIsDead)  // Check the flag here
        {
            this.gameObject.SetActive(false);

            if (vieObject != null)
            {
                vieObject.SetActive(true);
                LumiereVie.SetActive(true);
                LumiereVie2.SetActive(true);
                ActivationMains.SetActive(true);
            }
        }
    }
}
