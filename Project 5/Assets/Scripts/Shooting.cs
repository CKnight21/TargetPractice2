using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;

    public GameObject barrel;

    public float damage = 100f;

    public float fireRate = 10f;

    public float nextTimeToFire = .5f;

    public float force = 10f;
    
    public Camera cameraFPS;

    public ParticleSystem muzzleFlash;

    public GameObject bulletImpact;

    public LayerMask player;

    public AudioSource audioSource;

    public AudioClip MuzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            
           // Instantiate(bullet, transform.position, Quaternion.identity);
        }

    }   

    void Shoot()
    {
        muzzleFlash.Play();

        audioSource.PlayOneShot(MuzzleFlash, 1f);


        RaycastHit hit;
        if (Physics.Raycast(cameraFPS.transform.position, cameraFPS.transform.forward, out hit, player))
        {
            Debug.Log(hit.transform.name);

           Enemy enemy = hit.transform.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal);
            }

           GameObject impactGameObj = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));

           Destroy(impactGameObj, 2f);
        }
    }
}
