using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow; // CameraFollow scripti

    void Start()
    {
        if (cameraFollow == null)
        {
            cameraFollow = FindObjectOfType<CameraFollow>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            cameraFollow.ShakeCamera(0.01f, 0.3f, vibrato: 5, randomness: 10f);
            // Shake tetikle
        }
    }
}