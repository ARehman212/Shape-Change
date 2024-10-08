using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdleSpawner : MonoBehaviour
{
    public GameObject[] herdlePrefabs;  // Array to hold multiple prefabs
    public float spawnInterval = 2f;    // Time between spawns
    public float minYScale = 0.5f;      // Minimum Y scale
    public float maxYScale = 2f;        // Maximum Y scale
    public float minZScale = 0.5f;      // Minimum Z scale
    public float maxZScale = 2f;        // Maximum Z scale
    public float raycastDistance = 10f;  // Distance to check for the path surface
    public float heightOffset = 0.5f;   // Height offset above the path

    private void Start()
    {
        InvokeRepeating("SpawnRandomHerdle", spawnInterval, spawnInterval);
    }

    void SpawnRandomHerdle()
    {
        // Pick a random prefab from the array
        int randomIndex = Random.Range(0, herdlePrefabs.Length);
        GameObject herdlePrefab = herdlePrefabs[randomIndex];

        // Set the spawn position at -100 on X-axis
        Vector3 spawnPosition = new Vector3(-100f, 10f, 0f); // Set Y high enough to reach the path

        // Use raycasting to find the path surface
        RaycastHit hit;
        if (Physics.Raycast(spawnPosition, Vector3.down, out hit, raycastDistance))
        {
            spawnPosition.y = hit.point.y + heightOffset; // Adjust Y position to be above the surface
        }

        // Instantiate the selected prefab
        GameObject herdleInstance = Instantiate(herdlePrefab, spawnPosition, Quaternion.identity);

        // Randomize the scale on Y and Z axes
        float randomYScale = Random.Range(minYScale, maxYScale);
        float randomZScale = Random.Range(minZScale, maxZScale);

        // Apply the new scale
        herdleInstance.transform.localScale = new Vector3(
            herdleInstance.transform.localScale.x,
            randomYScale,
            randomZScale
        );
    }
}