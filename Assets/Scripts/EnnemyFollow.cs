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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceDuJoueur = Vector2.Distance(player.position, transform.position);
        if (distanceDuJoueur < LigneDeVue && distanceDuJoueur > distanceDeTir){

            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

        }
        else if (distanceDuJoueur <= distanceDeTir && nextFire < Time.time){
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LigneDeVue);
        Gizmos.DrawWireSphere(transform.position, distanceDeTir);
    }
}
