using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;

    [System.Serializable]
    public class Wave
    {
        public ObjectPooler pool;
        public float SpawnTimer;
        public float SpawnInterval;
        public int objectPerWave;
        public int spawnedObjectsCount;
    }
   
    void Update()
    {
        waves[waveNumber].SpawnTimer -= GameManager.Instance.adjustedWorldSpeed;
        if(waves[waveNumber].SpawnTimer <= 0)
        {
            waves[waveNumber].SpawnTimer += waves[waveNumber].SpawnInterval;
            SpawnObject();
        }
        if(waves[waveNumber].spawnedObjectsCount >= waves[waveNumber].objectPerWave)
        {
            waves[waveNumber].spawnedObjectsCount = 0;
            waveNumber++;
            if(waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }
    }

    private void SpawnObject()
    {
        GameObject spawnedObject = waves[waveNumber].pool.GetPooledObject();
        spawnedObject.transform.position = RandomSpawnPoint();
        //spawnedObject.transform.rotation = transform.rotation;
        spawnedObject.SetActive(true);
        waves[waveNumber].spawnedObjectsCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
        spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);

        return spawnPoint;
    }
}
