using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{
    public float followSpeed = 3f;
    public float stopDistance = 2f;
    public float separationDistance = 1.5f;
    public float separationAmount = 0.5f;

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

        // Player'a yönelme (sadece stopDistance dışında ise)
        Vector3 toPlayer = player.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;

        if (distanceToPlayer > stopDistance)
        {
            moveDirection += toPlayer.normalized;
        }

        // Hareket vektörü uygula (sabit hızla ama mesafeye saygılı)
        Vector3 newPosition = transform.position + moveDirection.normalized * followSpeed * Time.deltaTime;

        // Y eksenini player'ın altına düşürme
        newPosition.y = Mathf.Max(newPosition.y, player.position.y+0.5f);

        transform.position = newPosition;
    }
}