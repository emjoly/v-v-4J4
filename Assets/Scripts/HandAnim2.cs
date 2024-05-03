using System.Collections;
using UnityEngine;

public class HandAnim2 : MonoBehaviour
{
    public float speed = 10.0f;
    private bool isMoving = false;
    private Vector3 initialPosition;
    private float moveStartTime;

    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(StartHandRoutine());
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
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
            transform.position = initialPosition;
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
