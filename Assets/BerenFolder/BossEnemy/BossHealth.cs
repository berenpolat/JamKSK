using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthBarSlider;
    public Transform healthBarCanvas;
    public Vector3 offset = new Vector3(0, 2f, 0);

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
            healthBarCanvas.position = transform.position + offset;
            healthBarCanvas.forward = Camera.main.transform.forward;
        }
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("CAN AZALDI");
        currentHealth -= amount;

        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }

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
        LevelManager.Instance.LoadNextLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Boss hit by bullet!");

            // Bullet ile çarpışınca canın %20'sini kaybetsin
            float damageAmount = maxHealth * 0.2f;
            TakeDamage(damageAmount);

            // (Opsiyonel) bullet'ı da yok etmek istersen:
            Destroy(other.gameObject);
        }
    }
}