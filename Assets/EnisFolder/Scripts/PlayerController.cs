using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravity = -20f;
    public float dashSpeed = 15f;         // Dash sırasında kullanılacak hız
    public float dashDuration = 0.2f;     // Dash ne kadar sürsün

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private bool isDashing = false;
    private float dashTime = 0f;
    private Vector3 dashDirection;

    public bool canDash;

    [SerializeField] private GameObject dashPowerUp;
    private GameObject currentDashPowerUp;
    private bool dashPowerUpShows;
    void Start()
    {
        canDash = false;
        controller = GetComponent<CharacterController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "dasher")
        {
            currentDashPowerUp = Instantiate(dashPowerUp, transform.position, Quaternion.identity);
            canDash = true;
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        // Zemin kontrolü
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // DASH kontrolü
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing && canDash)
        {
            StartDash();
        }

        if (isDashing)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false;
                canDash = false;
            }
            return; // Dash sırasında normal hareketi atla
        }

        // Sağ/sol hareket
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(horizontalInput, 0f, 0f);
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

    void StartDash()
    {
        Destroy(currentDashPowerUp);
        isDashing = true;
        dashTime = dashDuration;

        // Bakış yönüne göre dash yönünü belirle
        if (transform.rotation.eulerAngles.y == 90)
            dashDirection = Vector3.right;
        else
            dashDirection = Vector3.left;
    }
}
