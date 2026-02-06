using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraumaTriggerSpawner : MonoBehaviour
{
    [Header("Prefab a spawnear")]
    public GameObject traumaPrefab;

    [Header("Spawn")]
    public Transform spawnPoint;

    [Header("Despawn")]
    public float autoDespawnTime = 10f;

    GameObject spawnedInstance;
    bool hasSpawned = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Player"))
        {
            SpawnTrauma();
        }
    }

    void SpawnTrauma()
    {
        spawnedInstance = Instantiate(
            traumaPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        hasSpawned = true;

        if (autoDespawnTime > 0)
            StartCoroutine(DespawnAfterTime());
    }

    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(autoDespawnTime);

        DespawnTrauma();
    }

    public void DespawnTrauma()
    {
        if (spawnedInstance != null)
            Destroy(spawnedInstance);
    }
}