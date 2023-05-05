using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3f;
    public float deleteSpeed = 5f;
    public float damage = 100f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }

            IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(deleteSpeed);
        Destroy(gameObject);
    }
}
