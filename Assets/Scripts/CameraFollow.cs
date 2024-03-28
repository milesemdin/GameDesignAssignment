using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float followSmoothness;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;

    private void FixedUpdate()
    {
        Vector2 cameraMovementVector = new Vector2();

        cameraMovementVector.x = playerTransform.position.x - transform.position.x;
        cameraMovementVector.y = playerTransform.position.y - transform.position.y;

        cameraMovementVector.x -= xOffset;
        cameraMovementVector.y -= yOffset;

        cameraMovementVector /= followSmoothness;

        transform.Translate(cameraMovementVector);
    }
}
