using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 10f;
    public float turnSpeed = 100f;
    private float originalSpeed;

    private float movementInput;
    private float turnInput;

    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject doorObject;
    public GameObject loseTextObject; // Reference to the lose text object

    public float fuel = 100f; // Starting fuel level
    public float maxFuel = 100f; // Maximum fuel capacity

    public TextMeshProUGUI boostTimerText;
    public Slider fuelBar; // Reference to the UI Slider

    public float driftIntensity = 0.9f;  // How much the car slides during drifting
    public float lateralGrip = 0.5f;    // Lateral grip during drifting
    public float driftSteerFactor = 2f; // Increased steering sensitivity while drifting
    public float driftBoost = 2f;       // Amount of speed boost after drifting

    private bool isDrifting = false;    // Tracks whether the player is drifting

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = speed;

        count = 0;
        SetCountText();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false); // Ensure lose text is hidden at the start
        doorObject.SetActive(true);

        if (boostTimerText != null)
        {
            boostTimerText.gameObject.SetActive(false);
        }

        // Initialize the fuel bar
        if (fuelBar != null)
        {
            fuelBar.maxValue = maxFuel;
            fuelBar.value = fuel;
        }
    }

    public IEnumerator ActivateSpeedBoost(float boostAmount, float boostDuration)
    {
        if (boostTimerText != null)
        {
            boostTimerText.gameObject.SetActive(true);
        }

        speed *= boostAmount;

        float remainingTime = boostDuration;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (boostTimerText != null)
            {
                boostTimerText.text = "Boost: " + Mathf.CeilToInt(remainingTime) + "s";
            }
            yield return null;
        }

        speed = originalSpeed;
        if (boostTimerText != null)
        {
            boostTimerText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        movementInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        // Detect drift input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDrifting = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isDrifting = false;
            StartCoroutine(DriftBoost());
        }

        // Decrease fuel over time
        fuel -= Time.deltaTime * 5f; // Fuel drains at 5 units per second
        if (fuel < 0)
        {
            fuel = 0;
            HandleOutOfFuel(); // Trigger the lose condition
        }

        // Update the fuel bar
        if (fuelBar != null)
        {
            fuelBar.value = fuel;
        }
    }

    void FixedUpdate()
    {
        // Apply forward movement
        Vector3 forwardMove = transform.forward * movementInput * speed * Time.fixedDeltaTime;

        // Apply drift or normal movement
        if (isDrifting)
        {
            // Reduce lateral grip to simulate sliding
            Vector3 lateralVelocity = Vector3.Project(rb.velocity, transform.right);
            rb.velocity -= lateralVelocity * lateralGrip;

            // Increase turn sensitivity while drifting
            Quaternion driftTurn = Quaternion.Euler(0f, turnInput * turnSpeed * driftSteerFactor * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * driftTurn);

            // Apply reduced forward movement to maintain control
            rb.AddForce(forwardMove * driftIntensity, ForceMode.Acceleration);
        }
        else
        {
            // Normal driving behavior
            rb.MovePosition(rb.position + forwardMove);

            Quaternion normalTurn = Quaternion.Euler(0f, turnInput * turnSpeed * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * normalTurn);
        }
    }

    private IEnumerator DriftBoost()
    {
        speed += driftBoost; // Temporarily increase speed
        yield return new WaitForSeconds(1f); // Boost lasts 1 second
        speed = originalSpeed; // Reset speed
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("SpeedBoost"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(ActivateSpeedBoost(2f, 5f));
        }
        else if (other.gameObject.CompareTag("Fuel"))
        {
            other.gameObject.SetActive(false);
            AddFuel(20f); // Add 20 fuel (adjust as needed)
        }
    }

    public IEnumerator ApplySlowdown(float factor, float duration)
    {
        float originalSpeed = speed;
        speed *= factor; // Reduce speed

        Debug.Log($"Speed reduced to {speed}");

        yield return new WaitForSeconds(duration); // Wait for the slowdown duration

        speed = originalSpeed; // Restore original speed
        Debug.Log("Speed restored to original");
    }

    public void AddFuel(float amount)
    {
        fuel += amount;
        if (fuel > maxFuel)
        {
            fuel = maxFuel; // Cap fuel to the maximum capacity
        }

        // Update the fuel bar
        if (fuelBar != null)
        {
            fuelBar.value = fuel;
        }

        Debug.Log($"Fuel added. Current fuel: {fuel}");
    }

    private void HandleOutOfFuel()
    {
        Debug.Log("Out of fuel! You lose.");
        loseTextObject.SetActive(true); // Display lose text
        speed = 0; // Stop player movement
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == 4)
        {
            doorObject.SetActive(false);
        }
        if (count >= 12)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}
