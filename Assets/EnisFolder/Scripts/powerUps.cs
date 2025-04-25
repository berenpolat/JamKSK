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

        // Yeni pozisyonu hesapla
        Vector3 newPosition = transform.position + moveDirection.normalized * followSpeed * Time.deltaTime;

        // Oyuncunun Y seviyesinin altına inmesini engelle
        newPosition.y = Mathf.Max(newPosition.y, player.position.y);

        // Yeni pozisyona hareket et
        transform.position = newPosition;
    }
}