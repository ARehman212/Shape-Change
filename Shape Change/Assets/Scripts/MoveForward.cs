using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5f;   // Set a default speed
    private float boundary = -170f;   // Adjust boundary to match the leftward movement

    // Update is called once per frame (use FixedUpdate for smoother movement)
    void FixedUpdate()
    {
        // Move the object left over time
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Check if the object has crossed the left boundary, then destroy it
        if (transform.position.x < boundary)
        {
            Destroy(gameObject);
        }
    }
}