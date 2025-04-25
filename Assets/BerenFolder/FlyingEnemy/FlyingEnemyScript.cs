using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    public Transform ballSpawnPoint;        // Topun fırlayacağı nokta
    public GameObject ballPrefab;            // Top prefabı
    public float shootInterval = 1.5f;        // Kaç saniyede bir top atsın

    private Transform playerTransform;
    private bool playerInRange = false;
    private float shootTimer = 0f;

    void Update()
    {
        if (playerInRange && playerTransform != null)
        {
            // Sürekli player'a bak
            Vector3 lookPos = playerTransform.position;
            lookPos.y = transform.position.y; // Yalnızca yatay düzlemde
            transform.LookAt(lookPos);

            // Süre kontrolü
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                ShootAtPlayer();
                shootTimer = 0f;
            }
        }
    }

    void ShootAtPlayer()
    {
        if (ballPrefab == null || ballSpawnPoint == null || playerTransform == null) return;

        GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

        // BallScript üzerinden hedef gönder
        BallScript ballScript = ball.GetComponent<BallScript>();
        if (ballScript != null)
        {
            ballScript.SetTarget(playerTransform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}