using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Takip hızı

    public Transform playerTransform;
    private bool playerInRange = false;
    

    void Update()
    {
        if (playerInRange && playerTransform != null)
        {
            // Player’a doğru yönel ve ilerle
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Sadece yatay düzlemde bakmasını istiyorsan:
            Vector3 lookPos = playerTransform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
