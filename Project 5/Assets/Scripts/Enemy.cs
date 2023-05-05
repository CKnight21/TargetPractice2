using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float startingHealth = 100f;
    public float speed;
    public float stopDis;
    public float rotationSpeed;
    public float runAwayDis;
    public Transform player;
    public float range;

    public float timeShots;
    public float startTimeShots;

    Rigidbody rb;

    public ParticleSystem dieEnemy;

    public GameObject bullet;

    public GameObject doorToNextLevel;

    public AudioClip enemyShot;

    public AudioSource audioSource;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public float groundDrag;

    public bool sight;

    private void Start()
    {
        health = startingHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;


        timeShots = startTimeShots;
    }

    private void Update()
    {
        
            sight = Physics.CheckSphere(transform.position, range, whatIsGround);

        if (Vector3.Distance(transform.position, player.position) < range)
        {

        }
        if (Vector3.Distance(transform.position, player.position) > stopDis)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - player.transform.position), rotationSpeed * Time.deltaTime);
            

        }
        else if(Vector3.Distance(transform.position, player.position) < stopDis && Vector3.Distance(transform.position, player.position) > runAwayDis)
        {
            transform.position = this.transform.position;
        }
        else if(Vector3.Distance(transform.position, player.position) < runAwayDis)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

        }
        

        if(timeShots <= 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - player.transform.position), rotationSpeed * Time.deltaTime);
            Instantiate(bullet, transform.position, transform.rotation);
            timeShots = startTimeShots;
            audioSource.PlayOneShot(enemyShot, 1f);
        }
        else
        {
            timeShots -= Time.deltaTime;
        }
    }

    public void TakeDamage(float ammount)
    {
        health -= ammount;

        if(health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        Instantiate(dieEnemy, transform.position, Quaternion.identity);
        doorToNextLevel.SetActive(false);
        
    }
}
