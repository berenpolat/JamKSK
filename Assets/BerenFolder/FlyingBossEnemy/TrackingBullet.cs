using UnityEngine;

public class TrackingBullet : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target == null) return;

        // Hedefe doğru yönü hesapla
        Vector3 direction = (target.position - transform.position).normalized;

        // Eğer Y yönünde yukarı gitmeye çalışıyorsa, Y hareketini sıfırla
        if (direction.y > 0f)
        {
            direction.y = 0f;
            direction = direction.normalized; // yönü tekrar normalize et
        }

        // Sonuçta sadece yatay veya aşağıya hareket edecek
        transform.position += direction * speed * Time.deltaTime;
    }
}