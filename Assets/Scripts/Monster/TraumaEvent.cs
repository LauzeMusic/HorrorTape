using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraumaEvent : MonoBehaviour
{
    [Header("Spawn")]
    public float lifeTime = 5f;     // tiempo máximo visible
    public bool useTimer = true;

    [Header("Despawn Movement")]
    public Vector3 despawnOffset = new Vector3(0, 0.5f, 0);
    public float despawnMoveTime = 0.4f;

    [Header("Opcional")]
    public bool destroyOnDespawn = true;

    bool isActive = false;
    bool alreadyTriggered = false;
    Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        if (alreadyTriggered) return;

        alreadyTriggered = true;
        gameObject.SetActive(true);
        isActive = true;

        if (useTimer)
            StartCoroutine(LifeRoutine());
    }

    IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine(DespawnRoutine());
    }

    public void Despawn()
    {
        if (!isActive) return;
        StartCoroutine(DespawnRoutine());
    }

    IEnumerator DespawnRoutine()
    {
        isActive = false;

        Vector3 targetPos = transform.position + despawnOffset;
        float t = 0f;

        while (t < despawnMoveTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, t / despawnMoveTime);
            yield return null;
        }

        if (destroyOnDespawn)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            Despawn();
        }
    }
}