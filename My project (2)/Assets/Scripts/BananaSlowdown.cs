// Attach this script to the banana GameObject
using UnityEngine;

public class BananaSlowdown : MonoBehaviour
{
    public float slowdownDuration = 3f; // How long the slowdown lasts
    public float slowdownFactor = 0.5f; // Multiplier for the reduced speed (e.g., 0.5 is half speed)

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            // Try to get the PlayerController from the object itself or its parents
            PlayerController playerController = other.GetComponentInParent<PlayerController>();

            if (playerController != null)
            {
                Debug.Log("Slowing down the player...");
                playerController.StartCoroutine(playerController.ApplySlowdown(slowdownFactor, slowdownDuration));
            }

            Destroy(gameObject); // Optionally destroy the banana
        }
    }

}
