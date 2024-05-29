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
    public bool bossIsDead = false;
    private Animator animator;
    private float startTime;

    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 10, transform.position.z);
        fistRoutine = StartCoroutine(FistRoutine());
        animator = GetComponent<Animator>();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") && Time.time - startTime >= 10)
        {
            isMoving = false;
        }
    }

    public void StopFistRoutine()
    {
        if (fistRoutine != null)
        {
            StopCoroutine(fistRoutine);
            fistRoutine = null;
        }
        isMoving = false;
        StartCoroutine(HandleDeathSequence());
    }

    private IEnumerator HandleDeathSequence()
    {
        yield return new WaitForSeconds(3); 
        this.gameObject.SetActive(false);

        if (vieObject != null)
        {
            vieObject.SetActive(true);
            LumiereVie.SetActive(true);
            LumiereVie2.SetActive(true);
            ActivationMains.SetActive(true);
        }
    }

    IEnumerator FistRoutine()
    {
        while (true)
        {
            if (isFirstTime)
            {
                yield return new WaitForSeconds(2);
                isFirstTime = false;
            }

            if (bossIsDead)
            {
                StopFistRoutine();
                yield break;
            }

            isMoving = true;
            startTime = Time.time;

            while (Time.time - startTime < 10)
            {
                yield return null;
            }

            while (isMoving)
            {
                yield return null;
            }

            if (bossIsDead)
            {
                StopFistRoutine();
                yield break;
            }

            yield return new WaitForSeconds(4);

            if (bossIsDead)
            {
                StopFistRoutine();
                yield break;
            }

            isMoving = true;
        }
    }

    public void PlayHurtAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("handHurt");
        }
    }

    public void PlayDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("handDeath");
        }
    }
}
