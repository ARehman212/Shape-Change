using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnerOnPath2 : MonoBehaviour
{
    [Header("Prefabs to Spawn")]
    public List<GameObject> prefabs; // List of prefabs to spawn
    [Header("Path Points")]
    public List<Transform> pathPoints; // List of points defining the path

    [Header("Spawn Settings")]
    public float spawnInterval = 1.0f; // Time interval between spawning prefabs
    public bool loop = false; // Should the spawning loop or stop when finished
    public float fixedYPosition = 4.15f; // Set this to the Y position where prefabs should spawn

    private int currentIndex = 0; // Keeps track of which prefab to spawn next
    private bool isSpawning = false; // Controls spawning status

    void Start()
    {
        // Start spawning process if there are prefabs and path points assigned
        if (prefabs.Count > 0 && pathPoints.Count > 0)
        {
            StartCoroutine(SpawnPrefabsAlongPath());
        }
        else
        {
            Debug.LogError("Please assign prefabs and path points in the inspector.");
        }
    }

    IEnumerator SpawnPrefabsAlongPath()
    {
        isSpawning = true;
        while (isSpawning)
        {
            // Spawn prefabs along the path
            for (int i = 0; i < pathPoints.Count; i++)
            {
                // Get the path point's position, but override the Y position
                Vector3 spawnPosition = new Vector3(pathPoints[i].position.x, fixedYPosition, pathPoints[i].position.z);

                // Instantiate the current prefab at the adjusted position
                Instantiate(prefabs[currentIndex], spawnPosition, pathPoints[i].rotation);

                // Wait for the specified interval before spawning the next prefab
                yield return new WaitForSeconds(spawnInterval);

                // Move to the next prefab in the list, and loop back to the first if we reach the end
                currentIndex = (currentIndex + 1) % prefabs.Count;
            }

            // If looping is disabled, stop after spawning all prefabs
            if (!loop)
            {
                isSpawning = false;
            }
        }
    }
}