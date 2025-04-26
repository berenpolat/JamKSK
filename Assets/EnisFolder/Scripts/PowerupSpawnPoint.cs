using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawnPoint : MonoBehaviour
{
    [System.Serializable]
    public class SpawnInfo
    {
        public Transform spawnTransform;
        public GameObject currentObject; // Şu anda burada spawnlı obje
    }

    public List<SpawnInfo> spawnPoints = new List<SpawnInfo>(); // Tüm spawn noktaları ve üzerindeki objeler
    public List<GameObject> randomPrefabs = new List<GameObject>(); // Random atanacak prefablar listesi

    public float respawnDelay = 5f; // Yok olan obje yerine yeni prefab spawn etmek için gecikme süresi

    private void Start()
    {
        // Oyunun başında her spawn point'e bir prefab spawn edelim
        foreach (var spawn in spawnPoints)
        {
            SpawnNewPowerup(spawn);
        }
    }

    private void Update()
    {
        // Sürekli olarak spawn point'lerin objeleri duruyor mu diye kontrol edelim
        foreach (var spawn in spawnPoints)
        {
            if (spawn.currentObject == null)
            {
                // Eğer obje destroy edildiyse belli bir süre sonra tekrar spawnla
                StartCoroutine(RespawnAfterDelay(spawn));
            }
        }
    }

    private IEnumerator RespawnAfterDelay(SpawnInfo spawn)
    {
        // Zaten respawn bekliyorsa tekrar başlatma
        if (spawn.currentObject != null)
            yield break;

        yield return new WaitForSeconds(respawnDelay);

        // Hala boşsa yeni obje spawnla
        if (spawn.currentObject == null)
        {
            SpawnNewPowerup(spawn);
        }
    }

    private void SpawnNewPowerup(SpawnInfo spawn)
    {
        if (randomPrefabs.Count == 0)
        {
            Debug.LogWarning("Random Prefab listesi boş!");
            return;
        }

        int randomIndex = Random.Range(0, randomPrefabs.Count);
        GameObject newPowerup = Instantiate(randomPrefabs[randomIndex], spawn.spawnTransform.position, spawn.spawnTransform.rotation);
        spawn.currentObject = newPowerup;
    }
}
