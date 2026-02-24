using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockTrigger : MonoBehaviour
{
    public Door door;
    public AudioSource eventAudio;
    public float unlockDelay = 60f;

    bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            door.LockDoor();

            if (eventAudio != null)
                eventAudio.Play();

            Invoke(nameof(UnlockDoor), unlockDelay);
        }
    }

    void UnlockDoor()
    {
        door.UnlockDoor();
    }
}