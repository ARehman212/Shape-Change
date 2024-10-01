using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeChanger : MonoBehaviour
{
    public GameObject trianglePrefab;  // Prefab for triangle
    public GameObject spherePrefab;    // Prefab for sphere
    public GameObject cylinderPrefab;  // Prefab for cylinder
    private GameObject currentShape;   // The current player shape
    private Vector3 originalScale;     // Original scale of the player
    public float sizeChangeSpeed = 0.1f; // Speed of size change

    private Camera mainCamera;

    private void Start()
    {
        // Store the original scale of the player and keep the current shape as the original
        originalScale = transform.localScale;
        currentShape = gameObject; // Start with the original player shape

        // Reference the main camera
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Continue resizing the shape when dragging the mouse
        HandleResizing();

        // Change the player's appearance based on key presses
        HandleShapeChange();
    }

    private void HandleResizing()
    {
        // Change the player's size when dragging the mouse
        if (Input.GetMouseButton(0)) // Left mouse button held down
        {
            // Get the mouse movement in both the Y and X axes
            float mouseY = Input.GetAxis("Mouse Y") * sizeChangeSpeed;
            float mouseX = Input.GetAxis("Mouse X") * sizeChangeSpeed;

            // Apply Y-axis movement to the Y scale and X-axis movement to the Z scale
            currentShape.transform.localScale += new Vector3(0, mouseY, mouseX); // Change Y and Z scales

            // Clamp the Y scale to prevent it from going negative or too small
            currentShape.transform.localScale = new Vector3(
                currentShape.transform.localScale.x, // Keep X scale unchanged
                Mathf.Max(currentShape.transform.localScale.y, 0.1f), // Prevent Y scale from going below 0.1
                Mathf.Max(currentShape.transform.localScale.z, 0.1f) // Prevent Z scale from going below 0.1
            );
        }
    }

    private void HandleShapeChange()
    {
        // Change the player's appearance based on key presses
        if (Input.GetKeyDown(KeyCode.A)) // Press A for triangle
        {
            ChangeShape(trianglePrefab);
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Press S for sphere
        {
            ChangeShape(spherePrefab);
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Press D for cylinder
        {
            ChangeShape(cylinderPrefab);
        }
    }

    private void ChangeShape(GameObject newShapePrefab)
    {
        Vector3 currentPosition = currentShape.transform.position; // Save the current position
        Quaternion currentRotation = currentShape.transform.rotation; // Save the current rotation

        // Destroy the current shape only if it's not the original player object
        if (currentShape != null && currentShape != gameObject)
        {
            Destroy(currentShape);
        }

        // Instantiate the new shape at the saved position and rotation
        currentShape = Instantiate(newShapePrefab, currentPosition, currentRotation);
        currentShape.transform.localScale = originalScale; // Keep the original scale

        // Transfer the camera to the new shape
        mainCamera.transform.SetParent(currentShape.transform); // Attach the camera to the new shape
        mainCamera.transform.localPosition = new Vector3(0, 2, -5); // Adjust camera position relative to the new shape
        mainCamera.transform.localRotation = Quaternion.identity; // Reset the camera rotation to default

        // Transfer player movement/controls to the new shape if applicable
        currentShape.AddComponent<MoveForward>(); // Add your movement script to the new shape
    }
}