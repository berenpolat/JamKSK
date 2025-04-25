using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Vector3 targetDirection;
    public float speed = 4f; // Topun sabit hızı
    public float lifeTime = 4f;
    
    void Start()
    {
        // 4 saniye sonra kendini yok et
        Destroy(gameObject, lifeTime);
    }
    public void SetTarget(Vector3 targetPosition)
    {
        targetDirection = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        // Topu sabit hızla ileri taşı
        transform.position += targetDirection * speed * Time.deltaTime;
    }
    
}