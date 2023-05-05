using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableDamage : MonoBehaviour
{
    public int damage;

    public GameObject throwableImpact;

    public Camera cameraFPS;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            RaycastHit hit;


            if (Physics.Raycast(cameraFPS.transform.position, cameraFPS.transform.forward, out hit))
            {

                Enemy enemy = hit.transform.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Destroy(gameObject);
                 
                }

                if (hit.rigidbody != null)
                {
                    enemy.TakeDamage(damage);
                    hit.rigidbody.AddForce(-hit.normal);
                    //Destroy(gameObject);
                    
                }

                GameObject impactGameObj = Instantiate(throwableImpact, hit.point, Quaternion.LookRotation(hit.normal));

                //Destroy(throwableImpact, 2f);
                //DestroyImmediate(throwableImpact, true);
            }
        }
    }       

}
