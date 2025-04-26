using UnityEngine;

public class BossEnemyScript : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject spherePrefab;
    public float scatterForce = 2f;

    private bool canShoot = true; // Şu anda top atabilir mi?
    private float cooldownTimer = 0f;
    private float cooldownDuration = 5f; // 5 saniyelik bekleme süresi

    void Update()
    {
        if (playerTransform != null)
        {
            // Sadece X ekseninde mesafe hesapla
            float distanceX = Mathf.Abs(playerTransform.position.x - transform.position.x);

            if (canShoot && distanceX <= 100f && distanceX > 5f)
            {
                Debug.Log("HIABFSIJZ");
                // Boss ok atıyor
                ScatterSpheres();
                canShoot = false;
                cooldownTimer = 0f; // Cooldown başlasın
            }
            else if (!canShoot)
            {
                // Cooldown süresi çalışıyor
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= cooldownDuration)
                {
                    canShoot = true; // 5 saniye geçti, tekrar atabilir
                }
            }
            else if (distanceX <= 5f)
            {
                // Yakın saldırı
            }
        }
    }

    void ScatterSpheres()
    {
        int numberOfSpheres = 15;
        float angleStep = 360f / numberOfSpheres; // Her top arasında kaç derece fark olacak
        float scatterForce = 5f; // Burada doğrudan belirliyoruz, yukarıdaki değişkeni kullanmak istersen taşı

        for (int i = 0; i < numberOfSpheres; i++)
        {
            GameObject sphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);

            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Her bir top için açıyı hesapla
                float angle = i * angleStep;
                float radian = angle * Mathf.Deg2Rad; // Dereceyi radyana çevir

                // X ve Y eksenlerinde dairesel dağılım
                Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f).normalized;

                rb.AddForce(direction * scatterForce, ForceMode.Impulse);
            }

            Destroy(sphere, 5f);
        }
    }

}