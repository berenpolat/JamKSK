using UnityEngine;

public class FollowingEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Takip hızı

    private Transform playerTransform;
    private bool playerInRange = false;
    private float fixedY;

    // ------- Animator için eklenenler -------
    [SerializeField] private Animator animator;

    void Start()
    {
        fixedY = transform.position.y; // Enemy'nin başlangıç Y konumu
    }

    void Update()
    {
        if (playerInRange && playerTransform != null)
        {
            // Yalnızca XZ düzleminde yön bul
            Vector3 flatPlayerPos = new Vector3(playerTransform.position.x, fixedY, playerTransform.position.z);
            Vector3 direction = (flatPlayerPos - transform.position).normalized;

            // Hareket
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Animator için: Hareket varsa Run true, yoksa false
            animator.SetBool("run", direction.magnitude > 0.1f);

            // Sadece yatayda bak
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
            }

            // Y konumunu sabitle (ekstra koruma)
            Vector3 fixedPosition = transform.position;
            fixedPosition.y = fixedY;
            transform.position = fixedPosition;
        }
        else
        {
            // Oyuncu menzilden çıkarsa koşmayı durdur
            animator.SetBool("run", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerInRange = true;
        }
        if (other.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            //EFEKT EKLE MQ
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
