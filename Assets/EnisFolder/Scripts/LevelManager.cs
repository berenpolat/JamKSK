using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class LevelScene
{
#if UNITY_EDITOR
    public SceneAsset sceneAsset; // Sadece editörde görünür
#endif
    public string sceneName; // Çalışma zamanında kullanılacak

    // Sahne adı güncelleme fonksiyonu
#if UNITY_EDITOR
    public void UpdateSceneName()
    {
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
    }
#endif
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Levels")]
    public List<LevelScene> levels = new List<LevelScene>();

    [Header("Settings")]
    public int currentLevelIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // İstersen açık bırak, istersen kapat sahneye göre
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Başlangıçta şu anki sahneyi bulalım
        string currentSceneName = SceneManager.GetActiveScene().name;
        currentLevelIndex = levels.FindIndex(l => l.sceneName == currentSceneName);

        if (currentLevelIndex == -1)
        {
            Debug.LogError("Şu anki sahne levels listesinde bulunamadı! LevelManager --> Levels listesine sahneleri doğru ekledin mi?");
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex + 1 < levels.Count)
        {
            currentLevelIndex++;
            SceneManager.LoadScene(levels[currentLevelIndex].sceneName);
        }
        else
        {
            Debug.Log("Tüm levellar tamamlandı!");
        }
    }

    public void loadOldLevel()
    {
        if (currentLevelIndex > 0)
        {
            currentLevelIndex--;
            SceneManager.LoadScene(levels[currentLevelIndex].sceneName);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(levels[currentLevelIndex].sceneName);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Editörde sahne isimlerini otomatik güncelle
        foreach (var level in levels)
        {
            level.UpdateSceneName();
        }
    }
#endif
}
