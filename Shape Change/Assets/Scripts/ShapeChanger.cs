using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeChanger : MonoBehaviour
{
    public GameObject trianglePrefab;  // Prefab for triangle
    public GameObject spherePrefab;    // Prefab for sphere
    public GameObject cylinderPrefab;  // Prefab for cylinder
    private GameObject currentShape;   // The current player shape
    private Vector3 targetScale;       // Target scale for smoother scaling
    public float sizeChangeSpeed = 0.1f; // Speed of size change
    public float scaleSmoothSpeed = 5f; // Speed of scale smoothing
    public float cameraTransitionSpeed = 5f; // Speed for camera smoothing

    private Camera mainCamera;

    private void Start()
    {
        // Store the original scale of the player and keep the current shape as the original
        targetScale = transform.localScale;
        currentShape = gameObject; // Start with the original player shape

        // Reference the main camera
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Continue resizing the shape when dragging the mouse
        HandleResizing();

        // Smoothly update the shape's scale
        SmoothScale();

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

            // Update the target scale based on mouse input
            targetScale += new Vector3(0, mouseY, mouseX); // Change Y and Z scales

            // Clamp the target Y and Z scale to prevent them from going negative or too small
            targetScale = new Vector3(
                targetScale.x, // Keep X scale unchanged
                Mathf.Max(targetScale.y, 0.1f), // Prevent Y scale from going below 0.1
                Mathf.Max(targetScale.z, 0.1f)  // Prevent Z scale from going below 0.1
            );
        }
    }

    private void SmoothScale()
    {
        // Smoothly interpolate the current scale towards the target scale
        currentShape.transform.localScale = Vector3.Lerp(
            currentShape.transform.localScale,
            targetScale,
            Time.deltaTime * scaleSmoothSpeed
        );
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
        currentShape.transform.localScale = targetScale; // Keep the target scale

        // Smoothly transition the camera to the new shape
        StartCoroutine(SmoothCameraTransition());
    }

    private IEnumerator SmoothCameraTransition()
    {
        Vector3 targetCameraPosition = currentShape.transform.position + new Vector3(0, 2, -5); // Adjust camera position relative to the new shape
        Quaternion targetCameraRotation = Quaternion.identity; // Reset the camera rotation to default

        while (Vector3.Distance(mainCamera.transform.position, targetCameraPosition) > 0.1f)
        {
            // Smoothly move the camera towards the target position and rotation
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCameraPosition, Time.deltaTime * cameraTransitionSpeed);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetCameraRotation, Time.deltaTime * cameraTransitionSpeed);

            yield return null;
        }

        // Ensure the camera is perfectly aligned after the transition
        mainCamera.transform.position = targetCameraPosition;
        mainCamera.transform.rotation = targetCameraRotation;
    }
}