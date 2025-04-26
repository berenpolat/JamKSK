using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set;}
    
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravity = -20f;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public bool isDashing = false;
    private float dashTime = 0f;
    private Vector3 dashDirection;

    public bool canDash;

    [SerializeField] private GameObject dashPowerUp;
    private GameObject currentDashPowerUp;
    private bool dashPowerUpShows;

    // ------------- Animator için eklenenler -------------
    [SerializeField] private Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        dashPowerUpShows = false;
        canDash = false;
        controller = GetComponent<CharacterController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dasher"))
        {
            if (!dashPowerUpShows && currentDashPowerUp == null)
            {
                dashPowerUpShows = true;
                currentDashPowerUp = Instantiate(dashPowerUp, transform.position, Quaternion.identity);
                canDash = true;
                Destroy(other.gameObject);
            }
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

        if (!isDashing)
        {
            // Sağ/sol hareket
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 move = new Vector3(horizontalInput, 0f, 0f);
            controller.Move(move * moveSpeed * Time.deltaTime);

            // Animator için hareket kontrolü
            if (isGrounded)
            {
                animator.SetBool("Running", Mathf.Abs(horizontalInput) > 0.1f);
            }
            else
            {
                animator.SetBool("Running", false);
            }

            // Zıplama
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
                animator.SetTrigger("jump");
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

    void StartDash()
    {
        dashPowerUpShows = false;
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
