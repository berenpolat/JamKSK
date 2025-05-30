using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileShooter3D : MonoBehaviour
{
    public static ProjectileShooter3D Instance { get; set; }

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 15f;
    public float firePointRadius = 10f;

    public bool isProjectileArmed;
   

    private Camera cam;

    
    [SerializeField] private GameObject projectilePowerUp;
    [SerializeField] private GameObject swordPowerUp;
    

    private GameObject currentPowerUpInstance;
    public GameObject currentSwordPowerUp;

    // UI ve LevelEnder için
    public GameObject holdToInteractUI;
    public Image holdToInteractBar;

    private bool isNearLevelEnder = false;
    private float holdTime = 0f;
    public float requiredHoldTime = 2f;
    private GameObject currentLevelEnder;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        isProjectileArmed = false;
        
        holdToInteractUI.SetActive(false);
    }

    void Update()
    {
        FollowMouseOnXYPlane();

        if (Input.GetMouseButtonDown(0) && isProjectileArmed)
        {
            Shoot();
        }

        

        if (isNearLevelEnder)
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdTime += Time.deltaTime;
            
                float targetFillAmount = holdTime / requiredHoldTime;
                holdToInteractBar.fillAmount = Mathf.Lerp(holdToInteractBar.fillAmount, targetFillAmount, Time.deltaTime * 10f);

                if (holdTime >= requiredHoldTime)
                {
                    if (currentLevelEnder != null)
                    {
                        
                        Destroy(currentLevelEnder);
                        LevelManager.Instance.LoadNextLevel();
                    }

                    holdToInteractUI.SetActive(false);
                    isNearLevelEnder = false;
                    holdTime = 0f;
                    holdToInteractBar.fillAmount = 0f;
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                holdTime = 0f;
                holdToInteractBar.fillAmount = 0f;
            }
        }
    }


    void FollowMouseOnXYPlane()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 centerPosition = new Vector3(transform.position.x, transform.position.y+2f, transform.position.z); // Y'yi 2f sabitledik
        Plane plane = new Plane(Vector3.forward, centerPosition);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPos = ray.GetPoint(distance);
            Vector3 direction = (mouseWorldPos - centerPosition).normalized;

            Vector3 offset = direction * firePointRadius;
            firePoint.position = centerPosition + offset;

            // Burayı değiştirdik:
            firePoint.LookAt(new Vector3(mouseWorldPos.x, firePoint.position.y, mouseWorldPos.z));
        }
    }






    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane xyPlane = new Plane(Vector3.forward, firePoint.position); // Z sabit, XY düzlemi

        if (xyPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);

            Vector3 targetDirection = (hitPoint - firePoint.position).normalized;
        
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(Vector3.forward, targetDirection));
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = targetDirection * projectileSpeed;
                isProjectileArmed = false;
                Destroy(currentPowerUpInstance);
            }
        }
    }




   

    private bool isProjectilePowerUpSpawning = false;
public bool isSwordPowerUpSpawning = false;

public void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("projectiler"))
    {
        if (!isProjectileArmed && currentPowerUpInstance == null && !isProjectilePowerUpSpawning)
        {
            isProjectilePowerUpSpawning = true; // İlk saniyede kilitle
            isProjectileArmed = true;
            currentPowerUpInstance = Instantiate(projectilePowerUp, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            Invoke(nameof(ResetProjectilePowerUpSpawning), 0.05f); // 0.05 saniye sonra aç
        }
    }

    if (other.CompareTag("sworder"))
    {
        if (!swordObj.Instance.isSwordArmed)
        {
            swordObj.Instance.isSwordArmed = true;
            currentSwordPowerUp = Instantiate(swordPowerUp, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    if (other.CompareTag("Ball") && !PlayerController.Instance.isDashing)
    {
        LevelManager.Instance.RestartLevel();
    }

    if (other.CompareTag("Enemy") && !PlayerController.Instance.isDashing)
    {
        ButtonController.Instance.levelFailPanel.SetActive(true);
    }

    if (other.CompareTag("trap") && !PlayerController.Instance.isDashing)
    {
        ButtonController.Instance.levelFailPanel.SetActive(true);
    }

    if (other.CompareTag("levelEnder"))
    {
        isNearLevelEnder = true;
        holdToInteractUI.SetActive(true);
        currentLevelEnder = other.gameObject;
    }
}

private void ResetProjectilePowerUpSpawning()
{
    isProjectilePowerUpSpawning = false;
}


public void OnTriggerExit(Collider other)
{
    if (other.CompareTag("levelEnder"))
    {
        isNearLevelEnder = false;
        holdToInteractUI.SetActive(false);
        holdTime = 0f;
        holdToInteractBar.fillAmount = 0f;
        currentLevelEnder = null;
    }
}

}
