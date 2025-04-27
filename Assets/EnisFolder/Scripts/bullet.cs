using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow; // CameraFollow scripti
    [SerializeField] private bool isSword ;

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
            if (!isSword)
            {
                Destroy(gameObject);    
            }
            
            cameraFollow.ShakeCamera(0.01f, 0.3f, vibrato: 5, randomness: 10f);
            // Shake tetikle
        }
    }
}