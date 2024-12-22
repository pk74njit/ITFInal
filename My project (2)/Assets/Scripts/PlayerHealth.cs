using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;      // Maximum health value
    private int currentHealth;       // Current health value

    public Slider healthBar;        
    public int damageAmount = 10;    

    public GameObject loseText;      

    void Start()
    {
        currentHealth = maxHealth;           
        healthBar.maxValue = maxHealth;     
        healthBar.value = currentHealth;     
        loseText.SetActive(false);           
    }

  
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;     
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        healthBar.value = currentHealth;   

     
        if (currentHealth <= 0)
        {
            Die();
        }
    }

   
   
    private void Die()
    {
        Debug.Log("Player has died!");
        loseText.SetActive(true);          
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

    // Handle collision with damage-causing objects
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Hazard"))
        {
            TakeDamage(damageAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Hazard"))
        {
            TakeDamage(damageAmount); 
        }
    }
}
