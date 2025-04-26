using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IdleEnemyScript : MonoBehaviour
{
    public float forceAmount = 10f;

    [SerializeField] private  float distance;
    private Rigidbody rb;
    private Vector3 startPosition;
    private bool goingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        // İlk sola hareket
        rb.AddForce(Vector3.left * forceAmount, ForceMode.Impulse);
        FlipSprite(false); // sola bak
    }

    void FixedUpdate()
    {
        float offset = transform.position.x - startPosition.x;

        if (!goingRight && offset <= -distance)
        {
            goingRight = true;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.right * forceAmount, ForceMode.Impulse);
            FlipSprite(true); // sağa bak
        }
        else if (goingRight && offset >= distance)
        {
            goingRight = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.left * forceAmount, ForceMode.Impulse);
            FlipSprite(false); // sola bak
        }
    }

    // Sprite'ı döndürme (2D için)
    void FlipSprite(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (faceRight ? 1 : -1);
        transform.localScale = scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            //EFEKT EKLE MQ
        }
    }
}