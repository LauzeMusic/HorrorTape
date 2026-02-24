using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeEventTrigger : MonoBehaviour
{
    [Header("Cassette que dispara este evento")]
    public NarrativeAudioData targetAudio;

    [Header("Delay después de iniciar audio")]
    public float delay = 60f;

    [Header("Puerta a abrir")]
    public Door doorToOpen;

    [Header("GameObjects a activar")]
    public GameObject[] objectsToActivate;

    bool triggered = false;

    CassettePlayer cassettePlayer;

    void Start()
    {
        cassettePlayer = FindObjectOfType<CassettePlayer>();
        StartCoroutine(WaitForAudio());
    }

    IEnumerator WaitForAudio()
    {
        while (true)
        {
            if (!triggered &&
                cassettePlayer.audioSource.clip == targetAudio.clip &&
                cassettePlayer.audioSource.isPlaying)
            {
                triggered = true;
                StartCoroutine(TriggerEventAfterDelay());
            }

            yield return null;
        }
    }

    IEnumerator TriggerEventAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // 🔓 Abrir puerta
        if (doorToOpen != null)
        {
            doorToOpen.UnlockDoor();
        }

        // 👁 Activar objetos (monstruo + cassette)
        foreach (GameObject go in objectsToActivate)
        {
            if (go != null)
                go.SetActive(true);
        }
    }
}