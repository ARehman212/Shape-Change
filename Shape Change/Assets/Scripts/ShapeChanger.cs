using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeChanger : MonoBehaviour
{
    private Vector3 targetScale;        // Target scale for smoother scaling
    public float sizeChangeSpeed = 0.1f; // Speed of size change
    public float scaleSmoothSpeed = 5f;  // Speed of scale smoothing
    public float moveSpeed = 5f;         // Speed for movement along the Z-axis
    private Camera mainCamera;
    private float zMinLimit = -2.4f;     // Minimum Z-axis position
    private float zMaxLimit = 2.4f;      // Maximum Z-axis position

    private void Start()
    {
        // Store the original scale of the object
        targetScale = transform.localScale;

        // Reference the main camera
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Continue resizing the shape when dragging the mouse
        HandleResizing();

        // Smoothly update the shape's scale
        SmoothScale();

        // Handle movement along the Z-axis with boundaries
        HandleMovement();
    }

    private void HandleResizing()
    {
        // Change the object's size when dragging the mouse
        if (Input.GetMouseButton(0)) // Left mouse button held down
        {
            // Get the mouse movement in both the Y and X axes
            float mouseY = Input.GetAxis("Mouse Y") * sizeChangeSpeed;
            float mouseX = Input.GetAxis("Mouse X") * sizeChangeSpeed;

            // Update the target scale based on mouse input
            targetScale += new Vector3(0, mouseY, mouseX); // Change Y and Z scales

            // Clamp the target Y and Z scale to a minimum of 1
            targetScale = new Vector3(
                targetScale.x,                    // Keep X scale unchanged
                Mathf.Max(targetScale.y, 1f),     // Y scale minimum of 1
                Mathf.Max(targetScale.z, 1f)      // Z scale minimum of 1
            );
        }
    }

    private void SmoothScale()
    {
        // Smoothly interpolate the current scale towards the target scale
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * scaleSmoothSpeed
        );
    }

    private void HandleMovement()
    {
        // Move the object along the Z-axis using the right and left arrow keys
        float moveZ = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // Calculate new Z position with boundary checks
        float newZPosition = Mathf.Clamp(transform.position.z + moveZ, zMinLimit, zMaxLimit);

        // Update the object's position within the allowed range
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);
    }
}