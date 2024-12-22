using UnityEngine;

public class CollisionPushBack : MonoBehaviour
{
    public float pushBackForce = 5f; // Adjust to increase or decrease pushback

    private void OnCollisionStay(Collision collision)
    {
        // Check if colliding with a building or wall
        if (collision.gameObject.CompareTag("Building") || collision.gameObject.CompareTag("Wall"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Calculate push-back direction
            Vector3 pushBackDirection = -collision.contacts[0].normal; // Opposite direction of the collision normal

            // Apply push-back force
            rb.AddForce(pushBackDirection * pushBackForce, ForceMode.Impulse);
        }
    }
}
