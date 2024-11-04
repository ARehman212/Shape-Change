using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float maxZPosition = 10f; // Maximum Z position limit
    public float minZPosition = -10f; // Minimum Z position limit

    private float lastTouchPositionX; // To store the last touch position on the X-axis

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Check if there are any touches on the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            // Only move the player if the touch phase is moved or began
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
            {
                // Calculate the movement based on the touch's X position
                float moveZ = (touch.position.x - lastTouchPositionX) * moveSpeed * Time.deltaTime;

                // Calculate the new position
                Vector3 movement = new Vector3(0, 0, moveZ);
                Vector3 newPosition = transform.position + movement;

                // Clamp the Z position to be within the specified limits
                newPosition.z = Mathf.Clamp(newPosition.z, minZPosition, maxZPosition);

                // Update the player's position
                transform.position = newPosition;
            }

            // Update lastTouchPositionX to the current touch position
            lastTouchPositionX = touch.position.x;
        }
    }
}