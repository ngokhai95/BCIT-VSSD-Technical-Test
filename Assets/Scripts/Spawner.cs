using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject modelToSpawn;
    [SerializeField] private readonly float offset = 5f;

    private void Start()
    {
        SpawnModel();
    }

    /// <summary>
    /// Spawns a model at the center of the screen.
    /// </summary>
   
    public void SpawnModel()
    {
        // Get the camera in the scene
        Camera mainCamera = Camera.main;

        // Check if the camera is not null before proceeding
        if (mainCamera != null)
        {
            // Calculate the center of the screen
            Vector3 center = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, offset));

            // Spawn the model at the center of the screen
            Instantiate(modelToSpawn, center, Quaternion.Euler(0f, 90f, 0f));
        }
        else
        {
            Debug.LogError("Camera is null. Unable to spawn model.");
        }
    }
}