using System.Collections.Generic;
using UnityEngine;

public class FlyingBossEnemyScript : MonoBehaviour
{
    public List<Transform> targetPoints;
    public float moveSpeed = 3f;
    public float reachThreshold = 0.1f;

    public Transform playerTransform;
    public GameObject spherePrefab;         // Dairesel mermi
    public GameObject trackingBulletPrefab; // Takip eden mermi
    public float scatterForce = 5f;

    private Transform currentTarget;

    private bool canShoot = true;
    private float shootCooldownTimer = 0f;
    [SerializeField ]private float shootCooldownDuration;

    void Start()
    {
        SetRandomTarget();
    }

    void Update()
    {
        // 1️⃣ Noktalar arasında hareket
        if (currentTarget != null && targetPoints.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentTarget.position) < reachThreshold)
            {
                SetRandomTarget();
            }
        }

        // 2️⃣ Shooting sistemi
        if (playerTransform != null)
        {
            float distanceX = Mathf.Abs(playerTransform.position.x - transform.position.x);

            if (canShoot && distanceX <= 30f)
            {
                RandomShooting(); // RANDOM olarak hangi saldırılar çalışacak?
                canShoot = false;
                shootCooldownTimer = 0f;
            }
            else if (!canShoot)
            {
                shootCooldownTimer += Time.deltaTime;
                if (shootCooldownTimer >= shootCooldownDuration)
                {
                    canShoot = true;
                }
            }
            
        }
    }

    void SetRandomTarget()
    {
        if (targetPoints.Count == 0) return;
        int index = Random.Range(0, targetPoints.Count);
        currentTarget = targetPoints[index];
    }

    void RandomShooting()
    {
        // Her saldırı tipi için %50 şans
        bool doScatter = Random.value > 0.5f;
        bool doTracking = Random.value > 0.5f;

        if (doScatter)
        {
            ScatterSpheres();
        }

        if (doTracking)
        {
            ShootTrackingBullet();
        }
    }

    void ScatterSpheres()
    {
        int numberOfSpheres = 15;
        float angleStep = 360f / numberOfSpheres;

        for (int i = 0; i < numberOfSpheres; i++)
        {
            GameObject sphere = Instantiate(spherePrefab, new Vector3(transform.position.x, transform.position.y,0) , Quaternion.identity);

            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float angle = i * angleStep;
                float radian = angle * Mathf.Deg2Rad;

                Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f).normalized;
                rb.AddForce(direction * scatterForce, ForceMode.Impulse);
            }

            Destroy(sphere, 5f);
        }
    }

    void ShootTrackingBullet()
    {
        GameObject bullet = Instantiate(trackingBulletPrefab, transform.position, Quaternion.identity);
        TrackingBullet trackingScript = bullet.GetComponent<TrackingBullet>();
        if (trackingScript != null)
        {
            trackingScript.SetTarget(playerTransform);
        }

        Destroy(bullet, 1f);
    }
}
