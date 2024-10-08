using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isGameActive = false; // Flag to check if the game is active

    public void StartGame()
    {
        isGameActive = true; // Set the flag to true
        Time.timeScale = 1;   // Resume the game
        Debug.Log("Game Started"); // Debug log to track the start
    }

    public void StopGame()
    {
        isGameActive = false; // Set the flag to false
        Time.timeScale = 0;   // Pause the game
        Debug.Log("Game Stopped");
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }
}