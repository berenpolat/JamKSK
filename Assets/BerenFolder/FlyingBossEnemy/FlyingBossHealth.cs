using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthBarSlider;
    public Transform healthBarCanvas; // Sağlık barının world-space canvas'ı
    public Vector3 offset = new Vector3(0, 2f, 0); // Boss'un üstünde nerede dursun

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = maxHealth;
        }
    }

    void Update()
    {
        if (healthBarCanvas != null)
        {
            // Health bar boss'un üstünde dursun
            healthBarCanvas.position = transform.position + offset;

            // Health bar hep kameraya baksın
            healthBarCanvas.forward = Camera.main.transform.forward;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0f)
            currentHealth = 0f;

        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("FlyingBoss hit by bullet!");

            // Bullet ile çarpışınca canın %20'sini kaybetsin
            float damageAmount = maxHealth * 0.2f;
            TakeDamage(damageAmount);

            // Bullet'ı yok et
            Destroy(other.gameObject);
        }
    }
}