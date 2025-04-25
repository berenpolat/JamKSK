using UnityEngine;

public class BossEnemyScript : MonoBehaviour
{
    public Transform playerTransform; // Player objesinin transformu

    void Update()
    {
        if (playerTransform != null)
        {
       
            float distanceX = Mathf.Abs(playerTransform.position.x - transform.position.x);

            if (distanceX <= 10f && distanceX > 5f)
            {
                // boss ok atıyor
            }
            else if (distanceX <= 5f)
            {
                // Yakın saldırı
            }
        }
    }
}