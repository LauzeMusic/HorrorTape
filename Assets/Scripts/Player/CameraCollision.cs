using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour

{
    public Transform cameraPivot;
    public float minDistance = 0.1f;
    public float maxDistance = 0.5f;
    public float smooth = 10f;
    public LayerMask collisionMask = ~0;

    float currentDistance;

    void Start()
    {
        currentDistance = maxDistance;
    }

    void LateUpdate()
    {
        Vector3 dir = (transform.position - cameraPivot.position).normalized;

        if (Physics.Raycast(cameraPivot.position, dir, out RaycastHit hit, maxDistance, collisionMask))
        {
            currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            currentDistance = maxDistance;
        }

        Vector3 targetPos = cameraPivot.position + dir * currentDistance;
        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.deltaTime);
    }
}
