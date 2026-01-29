using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCinematic : MonoBehaviour
{
    public PlayerController player;

    public Transform startPoint;
    public Transform endPoint;
    public float walkSpeed = 1.5f;

    void Start()
    {
        StartCoroutine(CinematicRoutine());
    }

    IEnumerator CinematicRoutine()
    {
        // esperamos a que termine el loading
        yield return new WaitUntil(() => player.currentState == PlayerController.PlayerState.Cinematic);

        player.transform.position = startPoint.position;
        player.transform.rotation = startPoint.rotation;

        while (Vector3.Distance(player.transform.position, endPoint.position) > 0.05f)
        {
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                endPoint.position,
                walkSpeed * Time.deltaTime
            );

            yield return null;
        }

        player.ExitCinematic();
    }
}