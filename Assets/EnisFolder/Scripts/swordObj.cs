using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordObj : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
