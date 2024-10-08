using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10f; // Set a default speed
    private float boundary = 90f; // Adjust boundary to match the leftward movement
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>(); // Find the GameController
    }

    void FixedUpdate()
    {
        {
            // Move the object left over time
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            Debug.Log("Moving forward"); // Debug log for movement

            // Check if the object has crossed the left boundary, then destroy it
            if (transform.position.x > boundary)
            {
                Destroy(gameObject);
            }
        }
    }
}