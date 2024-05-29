using System.Collections;
using UnityEngine;

public class HandAnim : MonoBehaviour
{
    public float speed = 10.0f;
    public bool isMoving = false;
    private Vector3 initialPosition;
    private float moveStartTime;
    private Coroutine handRoutine;
    private Boss boss;
    private Animator animator;

    void Start()
    {
        initialPosition = transform.position;
        boss = GetComponentInParent<Boss>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        handRoutine = StartCoroutine(StartHandRoutine());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        if (isMoving && boss.currentHealth > 0)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    IEnumerator StartHandRoutine()
    {
        yield return new WaitForSeconds(2);
        handRoutine = StartCoroutine(HandRoutine());
    }

    IEnumerator HandRoutine()
    {
        while (true)
        {
            isMoving = true;
            moveStartTime = Time.time;
            yield return new WaitUntil(() => Time.time - moveStartTime > 15);

            isMoving = false;
            yield return new WaitForSeconds(4);
        }
    }

    public void StopHandRoutine()
    {
        if (handRoutine != null)
        {
            StopCoroutine(handRoutine);
            handRoutine = null;
        }
        isMoving = false;
        StartCoroutine(HandleDeathSequence());
    }

    private IEnumerator HandleDeathSequence()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }

    public void PlayHurtAnimation()
    {
        if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("handDommage"))
        {
            animator.SetTrigger("handDommage");
            animator.SetBool("isHurt", true);
            StartCoroutine(ResetHurtAfterDelay());
        }
    }

    public void PlayDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("handDeath");
        }
    }

    private IEnumerator ResetHurtAfterDelay()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        ResetHurt();
    }

    public void ResetHurt()
    {
        if (animator != null)
        {
            animator.SetBool("isHurt", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" && isMoving)
        {
            transform.position = initialPosition;
        }
    }
}
