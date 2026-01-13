using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorTrigger : MonoBehaviour
{
    public TerrorSFX terrorSFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            terrorSFX.StartTerror();
        }
    }
}