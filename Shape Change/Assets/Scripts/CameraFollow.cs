using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject[] shapePrefabs;  // Array to hold different shape prefabs
    public float spawnInterval = 3f;   // Time between each spawn
    public float spawnDistance = 20f;  // Distance from the player where shapes spawn
    public Vector2 scaleRange = new Vector2(0.5f, 3f);  // Random range for shape scale
    public float moveSpeed = 5f;  // Speed at which the shape moves towards the player

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnShapes()); // Start spawning shapes
    }

    private IEnumerator SpawnShapes()
    {
        while (true)
        {
            SpawnShape();  // Spawn a new shape
            yield return new WaitForSeconds(spawnInterval);  // Wait for the next spawn
        }
    }

    private void SpawnShape()
    {
        // Choose a random shape from the prefab array
        int randomIndex = Random.Range(0, shapePrefabs.Length);
        GameObject shapePrefab = shapePrefabs[randomIndex];

        // Set the spawn position in front of the player
        Vector3 spawnPos = playerTransform.position + Vector3.forward * spawnDistance;

        // Instantiate the shape at the spawn position
        GameObject newShape = Instantiate(shapePrefab, spawnPos, Quaternion.identity);

        // Randomize the scale of the shape
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        newShape.transform.localScale = new Vector3(1, randomScale, randomScale); // Adjust X or Z if needed

        // Add movement script or logic to make it move towards the player
        newShape.AddComponent<ShapeMover>().SetMoveSpeed(moveSpeed);
    }
}