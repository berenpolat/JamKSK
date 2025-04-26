using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordObj : MonoBehaviour
{
    
    public static swordObj Instance { get; set; }
    public bool isSwordArmed;
    [SerializeField] private Animator animator;
    public GameObject swordObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isSwordArmed = false;
        swordObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isSwordArmed)
        {
            Sword();
        }
    }
    void Sword()
    {
        ProjectileShooter3D.Instance.ResetSwordPowerUpSpawning();
        Destroy(ProjectileShooter3D.Instance.currentSwordPowerUp);
        swordObject.SetActive(true);
        animator.SetTrigger("sword");
    }
}
