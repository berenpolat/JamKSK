using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{
    public float followSpeed = 3f;
    public float stopDistance = 2f;
    public float separationDistance = 1.5f;   // Minimum mesafe
    public float separationAmount = 0.5f;     // Uzaklaşma miktarı

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 moveDirection = Vector3.zero;

        // Player'a yönelme
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > stopDistance)
        {
            moveDirection += (player.position - transform.position).normalized;
        }

        // Diğer power-uplardan uzak durma
        GameObject[] allPowerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach (GameObject other in allPowerUps)
        {
            if (other != this.gameObject)
            {
                float dist = Vector3.Distance(transform.position, other.transform.position);
                if (dist < separationDistance)
                {
                    Vector3 pushDir = (transform.position - other.transform.position).normalized;
                    moveDirection += pushDir * separationAmount;
                }
            }
        }

        // Hareket ettir
        transform.position += moveDirection.normalized * followSpeed * Time.deltaTime;

        // Ground check - yere çok yakınsa yukarı taşı
        Ray groundRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(groundRay, out RaycastHit hit, 1f, LayerMask.GetMask("ground")))
        {
            float groundDistance = hit.distance;
            if (groundDistance < 0.5f)
            {
                transform.position += Vector3.up * (0.3f - groundDistance); // 0.3 yüksekliğe sabitler
            }
        }
    }
}
