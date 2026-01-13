using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorSFX : MonoBehaviour
{
    [Header("SFX Objects (inactivos)")]
    public GameObject[] sfxObjects;

    [Header("Timing")]
    public float minDelay = 10f;
    public float maxDelay = 40f;

    bool active = false;

    public void StartTerror()
    {
        if (!active)
        {
            active = true;
            StartCoroutine(TerrorLoop());
        }
    }

    IEnumerator TerrorLoop()
    {
        while (active)
        {
            float wait = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(wait);

            GameObject sfx = sfxObjects[Random.Range(0, sfxObjects.Length)];
            AudioSource src = sfx.GetComponent<AudioSource>();

            if (src == null) continue;

            sfx.SetActive(true);
            src.Play();

            yield return new WaitForSeconds(src.clip.length);
            sfx.SetActive(false);
        }
    }

    public void StopTerror()
    {
        active = false;
        StopAllCoroutines();
    }
}