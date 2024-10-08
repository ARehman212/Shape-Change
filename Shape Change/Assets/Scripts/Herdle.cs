using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herdle : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        // Find the GameController in the scene
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Call the StopGame method in the GameController
            gameController.StopGame();
            Debug.Log("Game Over");
        }
    }
}