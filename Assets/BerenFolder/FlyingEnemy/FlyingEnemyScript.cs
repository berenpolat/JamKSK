using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    public Transform ballSpawnPoint;        // Topun fırlayacağı nokta
    public GameObject ballPrefab;           // Top prefabı
    public float shootInterval = 1.5f;      // Kaç saniyede bir top atsın

    private Transform playerTransform;
    private bool playerInRange = false;
    private float shootTimer = 0f;
    private float fixedY; // sabit Y yüksekliği

    [SerializeField] private Animator animator;
    void Start()
    {
        fixedY = transform.position.y; // oyuna başladığı andaki Y yüksekliği sabitlenir
    }

    void Update()
    {
        if (playerInRange && playerTransform != null)
        {
            // Sadece yatay düzlemde (X-Z) player'a bak
            Vector3 flatPlayerPos = new Vector3(playerTransform.position.x, fixedY, playerTransform.position.z);
            transform.LookAt(flatPlayerPos);

            // Top fırlatma zaman kontrolü
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                ShootAtPlayer();
                shootTimer = 0f;
            }

            // Y pozisyonunu sabit tut (yere yapışık kalması için)
            Vector3 currentPos = transform.position;
            currentPos.y = fixedY;
            transform.position = currentPos;
        }
    }

    void ShootAtPlayer()
    {
        if (ballPrefab == null || ballSpawnPoint == null || playerTransform == null) return;

        GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

        BallScript ballScript = ball.GetComponent<BallScript>();
        if (ballScript != null)
        {
            animator.SetTrigger("shoot");
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
