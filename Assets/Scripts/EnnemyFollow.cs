using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyFollow : MonoBehaviour
{
    public float speed;
    public Transform player;
    public float LigneDeVue;
    public float distanceDeTir;
    public GameObject bullet;
    public GameObject bulletParent;
    public float fireRate = 1f;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
{
    // If player is null (in case it gets destroyed), find the new player
    if (player == null)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Now check if player is not null before proceeding
    if (player != null)
    {
        float distanceDuJoueur = Vector2.Distance(player.position, transform.position);
        if (distanceDuJoueur < LigneDeVue && distanceDuJoueur > distanceDeTir)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceDuJoueur <= distanceDeTir && nextFire < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LigneDeVue);
        Gizmos.DrawWireSphere(transform.position, distanceDeTir);
    }
}
