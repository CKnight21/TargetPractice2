using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth;

    public int damage;

    public HealthBar healthBar;

    public Collision col;

    public GameObject enemyBullet;

    public GameObject gameOverScreen;

    public AudioSource audioSource;
    public AudioClip hit;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(damage);

        }
      
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(damage);
            audioSource.PlayOneShot(hit, 1f);


        }
        if (collision.gameObject.tag == "Player")
        {
            TakeDamage(damage);
            audioSource.PlayOneShot(hit, 1f);

        }
        if (CompareTag("EnemyBullet"))
        {
            OnCollisionEnter(col);
            TakeDamage(damage);
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            gameOverScreen.SetActive(true);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
