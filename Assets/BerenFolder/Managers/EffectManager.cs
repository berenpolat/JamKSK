using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public void InitializeEffect(Transform targetTransform,GameObject effectPrefab)
    {
      
            if (targetTransform != null && effectPrefab != null)
            {
                Instantiate(effectPrefab, targetTransform.position, Quaternion.identity);
            }
        }
    }

