using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    public float fuelAmount = 20f; // Amount of fuel restored

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that triggered the collider is tagged as 'Player'
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                Debug.Log("Fuel picked up!");
                playerController.AddFuel(fuelAmount);
            }

            // Destroy the fuel pickup object after collection
            Destroy(gameObject);
        }
    }
}
