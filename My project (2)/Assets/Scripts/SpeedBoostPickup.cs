using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    public float speedBoostAmount = 2f; 
    public float duration = 5f;        

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                StartCoroutine(playerController.ActivateSpeedBoost(speedBoostAmount, duration));
            }

      
            gameObject.SetActive(false);
        }
    }
}
