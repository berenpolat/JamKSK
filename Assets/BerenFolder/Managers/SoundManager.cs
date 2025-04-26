using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEntry
    {
        public AudioSource audioSource; // Sesin çalınacağı ses kaynağı
        public AudioClip audioClip;     // Çalınacak ses dosyası
    }

    public List<SoundEntry> soundsList = new List<SoundEntry>();

    void Start()
    {
        // Örnek: Başlangıçta listedeki tüm sesleri çalmak istersen:
        foreach (SoundEntry entry in soundsList)
        {
            if (entry.audioSource != null && entry.audioClip != null)
            {
                entry.audioSource.clip = entry.audioClip;
                entry.audioSource.Play();
            }
        }
    }

    void Update()
    {
        // İstersen Update içinde başka triggerlar ile sesleri tetikleyebiliriz.
    }
}