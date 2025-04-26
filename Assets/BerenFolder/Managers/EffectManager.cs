using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [System.Serializable]
    public class EffectEntry
    {
        public Transform targetTransform; // Efektin çıkacağı obje
        public GameObject effectPrefab;   // Çıkacak efekt prefabı
    }

    public List<EffectEntry> effectsList = new List<EffectEntry>();

    void Start()
    {
        // Örnek: Başlangıçta hepsinin üstünde efekt başlatmak istersen:
        foreach (EffectEntry entry in effectsList)
        {
            if (entry.targetTransform != null && entry.effectPrefab != null)
            {
                Instantiate(entry.effectPrefab, entry.targetTransform.position, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        // Buraya istersen efektleri tetiklemek için input vs koyabiliriz
    }
}