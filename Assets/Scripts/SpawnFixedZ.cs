using UnityEngine;

public class SpawnFixedZ : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab to spawn
    public float spawnDistance = 10f; // Fixed Z distance from the player

    void Update()
    {
        // Trigger instantiation with a key press
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        // Calculate the spawn position based on the player's position and fixed Z-distance
        Vector3 spawnPosition = transform.position + new Vector3(0, 0, spawnDistance);

        // Instantiate the object
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
