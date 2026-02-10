using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public AmbientVolumeController ambient;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            ambient.EnterHouse();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            ambient.ExitHouse();
    }
}