using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Reference to the player's transform
    public Vector3 offset;        // Offset between the camera and player
    public float smoothSpeed = 0.125f;  // Smooth speed for camera movement

    void LateUpdate()
    {
        // Desired position of the camera (player's position + offset)
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}