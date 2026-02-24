using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVanishOnLook : MonoBehaviour
{
    public float visibleTime = 1.5f;
    public GameObject cassetteToActivate; // opcional

    bool triggered = false;
    Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (triggered) return;

        if (IsVisible())
        {
            triggered = true;
            StartCoroutine(Vanish());
        }
    }

    bool IsVisible()
    {
        Vector3 viewportPos = playerCamera.WorldToViewportPoint(transform.position);

        bool inView =
            viewportPos.z > 0 &&
            viewportPos.x > 0 && viewportPos.x < 1 &&
            viewportPos.y > 0 && viewportPos.y < 1;

        return inView;
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(visibleTime);

        if (cassetteToActivate != null)
            cassetteToActivate.SetActive(true);

        gameObject.SetActive(false);
    }
}