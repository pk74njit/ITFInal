using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;       // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the camera follows the player
    public float rotationSpeed = 5f; // Speed at which the camera rotates to match the player

    private Vector3 offset;        // Offset between the camera and player

    void Start()
    {
        // Calculate and store the initial offset at the start
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Update the target position based on the player’s position and rotation
        Vector3 targetPosition = player.position + player.rotation * offset;

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Smoothly rotate the camera to match the player's rotation
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
