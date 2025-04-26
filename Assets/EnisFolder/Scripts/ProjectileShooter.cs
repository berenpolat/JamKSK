using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectileShooter3D : MonoBehaviour
{
    public static ProjectileShooter3D Instance { get; set;}
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 15f;
    public float firePointRadius = 1.5f;

    public bool isProjectileArmed;

    public bool isSwordArmed;

    private Camera cam;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject projectilePowerUp;
    [SerializeField] private GameObject swordPowerUp;
    public GameObject swordObject;

    
    private GameObject currentPowerUpInstance;
    private GameObject currentSwordPowerUp;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        isProjectileArmed = false;
        isSwordArmed = false;
        swordObject.SetActive(false);
        
    }

    void Update()
    {
        FollowMouseOnXYPlane();

        if (Input.GetMouseButtonDown(0) && isProjectileArmed)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && isSwordArmed)
        { 
            Sword();   
        }
    }

    void FollowMouseOnXYPlane()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Karakterin bulunduğu XY düzlemine ray gönder
        Plane plane = new Plane(Vector3.forward, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPos = ray.GetPoint(distance);

            Vector3 direction = (mouseWorldPos - transform.position).normalized;

            // FirePoint karakterin etrafında dairede döner
            Vector3 offset = direction * firePointRadius;
            firePoint.position = transform.position + offset;

            // FirePoint mouse yönüne dönsün
            firePoint.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // FirePoint'in yukarı yönüne doğru fırlat (LookRotation bu yöne bakıyor)
            rb.velocity = firePoint.up * projectileSpeed;
            isProjectileArmed = false;
            Destroy(currentPowerUpInstance);
        }
    }

    void Sword()
    {
        Destroy(currentSwordPowerUp);
        swordObject.SetActive(true);
        animator.SetTrigger("sword");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "projectiler" && isProjectileArmed == false)
        {
            isProjectileArmed = true;

            // İstenilen objeyi spawn et
            currentPowerUpInstance = Instantiate(projectilePowerUp, transform.position, Quaternion.identity);

            Destroy(other.gameObject);
        }

        if (other.transform.tag == "sworder")
        {
            isSwordArmed = true;

            currentSwordPowerUp = Instantiate(swordPowerUp, transform.position, quaternion.identity);
            Destroy(other.gameObject);
        }


        if (other.transform.tag == "Ball")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy" && !PlayerController.Instance.isDashing)
        {
            Destroy(gameObject);
        }
    }
}