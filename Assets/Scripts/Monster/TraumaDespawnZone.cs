using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraumaDespawnZone : MonoBehaviour
{
    public TraumaTriggerSpawner spawner;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawner.DespawnTrauma();
        }
    }
}