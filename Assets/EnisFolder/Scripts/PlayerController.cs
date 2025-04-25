using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Zemin kontrolü
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Karakterin yere yapışık kalmasını sağla
        }

        // Sağ/sol hareket
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(horizontalInput, 0f, 0f); // Sadece X ekseninde hareket
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Zıplama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Yerçekimi uygula
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Karakteri yöne döndür
        if (horizontalInput > 0)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (horizontalInput < 0)
            transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}