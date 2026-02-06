using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraumaRandomSpawner : MonoBehaviour
{
    [Header("Objetos que pueden aparecer")]
    public GameObject[] traumaObjects;

    [Header("Timing")]
    public float minDelay = 20f;
    public float maxDelay = 40f;
    public float visibleTime = 3f;

    [Header("Opcional")]
    public bool startOnPlay = true;

    void Start()
    {
        if (startOnPlay)
            StartCoroutine(TraumaRoutine());
    }

    IEnumerator TraumaRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            if (traumaObjects.Length == 0)
                yield break;

            // elegir uno aleatorio
            int index = Random.Range(0, traumaObjects.Length);
            GameObject selected = traumaObjects[index];

            // activar
            selected.SetActive(true);

            yield return new WaitForSeconds(visibleTime);

            // desactivar
            selected.SetActive(false);
        }
    }
}