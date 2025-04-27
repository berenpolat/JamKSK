using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set;}
    [System.Serializable]
    public class SoundEntry
    {
        public string name;             // Sesin ismi
        public AudioSource audioSource; // Sesin çalınacağı ses kaynağı
        public AudioClip audioClip;     // Çalınacak ses dosyası
        public bool isMusic;            // Bu ses arka plan müziği mi?
    }

    public List<SoundEntry> soundsList = new List<SoundEntry>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        // Sahne başladığında müzikleri otomatik çal
        foreach (SoundEntry entry in soundsList)
        {
            if (entry.isMusic && entry.audioSource != null && entry.audioClip != null)
            {
                entry.audioSource.clip = entry.audioClip;
                entry.audioSource.loop = true; // Müzikler döngüde çalsın
                entry.audioSource.Play();
            }
        }
    }

    /// <summary>
    /// Tek seferlik ses efekti çalar (OneShot).
    /// </summary>
    /// <param name="soundName">Çalınacak efektin ismi</param>
    public void PlaySoundEffect(string soundName)
    {
        SoundEntry entry = soundsList.Find(x => x.name == soundName && !x.isMusic);
        if (entry != null && entry.audioSource != null && entry.audioClip != null)
        {
            entry.audioSource.PlayOneShot(entry.audioClip);
        }
        else
        {
            Debug.LogWarning($"[SoundManager] Sound effect '{soundName}' bulunamadı.");
        }
    }

    /// <summary>
    /// İstediğin müziği çalar (loop opsiyonu var).
    /// </summary>
    /// <param name="musicName">Çalınacak müziğin ismi</param>
    /// <param name="loop">Müzik sonsuz döngüde mi çalsın?</param>
    public void PlayMusic(string musicName, bool loop = true)
    {
        SoundEntry entry = soundsList.Find(x => x.name == musicName && x.isMusic);
        if (entry != null && entry.audioSource != null && entry.audioClip != null)
        {
            entry.audioSource.clip = entry.audioClip;
            entry.audioSource.loop = loop;
            entry.audioSource.Play();
            Debug.Log("Ses oynuyor");
        }
        else
        {
            Debug.LogWarning($"[SoundManager] Music '{musicName}' bulunamadı.");
        }
    }
}
