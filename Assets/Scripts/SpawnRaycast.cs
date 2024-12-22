using UnityEngine;

public class SpawnRaycast : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab to spawn
    public LayerMask hitLayers;      // Layer mask to define which surfaces the ray can hit
    public float maxRayDistance = 50f; // Max distance for the raycast

    void Update()
    {
        // Trigger instantiation with a key press
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            SpawnObjectWithRaycast();
        }
    }

    void SpawnObjectWithRaycast()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits a surface within the defined layers
        if (Physics.Raycast(ray, out hit, maxRayDistance, hitLayers))
        {
            // Spawn the object at the hit point
            Instantiate(objectToSpawn, hit.point, Quaternion.identity);
        }
    }
}
