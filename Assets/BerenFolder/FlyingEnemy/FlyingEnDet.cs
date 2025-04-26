using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnDet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
            //EFEKT EKLE MQ
        }
    }
}