using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject roadSection;  // Reference to the road section prefab

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure the player triggers the event
        {
            // Calculate the spawn position with a fixed X value (-174) and ahead of the player on the Z-axis
            Vector3 spawnPosition = new Vector3(-174, 0, 0);

            // Instantiate the new road section at the calculated position
            Instantiate(roadSection, spawnPosition, Quaternion.identity);
        }
    }
}