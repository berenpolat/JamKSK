using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow; // CameraFollow scripti
    [SerializeField] private bool isSword;

    [Header("Effects")]
    [SerializeField] private GameObject swordEffectPrefab;
    [SerializeField] private GameObject defaultEffectPrefab;

    void Start()
    {
        isSword = false;

        if (cameraFollow == null)
        {
            cameraFollow = FindObjectOfType<CameraFollow>();
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.name == "PlayerSword")
        {
            isSword = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Önce efekt spawnla
            SpawnDeathEffect(other.transform.position);

            if (!isSword)
            {
                Destroy(gameObject);    
            }

            Destroy(other.gameObject); // Enemy'yi yok et
            cameraFollow.ShakeCamera(0.01f, 0.3f, vibrato: 5, randomness: 10f); // Shake tetikle
        }
    }

    private void SpawnDeathEffect(Vector3 spawnPosition)
    {
        GameObject selectedEffect = isSword ? swordEffectPrefab : defaultEffectPrefab;
        
        if (selectedEffect != null)
        {
            GameObject effect = Instantiate(selectedEffect, spawnPosition, Quaternion.identity);
            Destroy(effect, 2f); // Efekti 2 saniyede yok et (opsiyonel, sahneyi temiz tutmak için)
        }
        else
        {
            Debug.LogWarning("[Bullet] Efekt prefabı atanmadı!");
        }
    }
}