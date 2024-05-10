using System.Collections;
using UnityEngine;
 
public class HandAnim : MonoBehaviour
{
    public float speed = 10.0f;
    public bool isMoving = false;
    private Vector3 initialPosition;
    private float moveStartTime;
    public Boss boss;

    void Start()
    {
        initialPosition = transform.position;
        boss = GetComponent<Boss>();
    }
    void OnEnable()
    {
        StartCoroutine(StartHandRoutine());
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
        else if (boss.currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }


    IEnumerator StartHandRoutine()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(HandRoutine());
    }

    IEnumerator HandRoutine()
    {
        while (true)
        {
            isMoving = true;
            moveStartTime = Time.time;
            yield return new WaitUntil(() => Time.time - moveStartTime > 15);

            isMoving = false;

            yield return new WaitForSeconds(5);
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
