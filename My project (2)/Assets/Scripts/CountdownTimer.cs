using UnityEngine;
using TMPro; 

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 60f; 
    private float remainingTime; 

    public TextMeshProUGUI timerText;

    public GameObject gameOverText; 
    public GameObject winTextObject;
    public PlayerController playerController;

    private bool isGameOver = false;

    void Start()
    {
        remainingTime = startTime; 
        gameOverText.SetActive(false); 
    }

    void Update()
    {
       
        if (!isGameOver)
        {
            remainingTime -= Time.deltaTime; 

     
            timerText.text = "Time Left: " + Mathf.CeilToInt(remainingTime) + "s";

            
            if (remainingTime <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        isGameOver = true; 
        remainingTime = 0; 

      
        timerText.text = "Time Left: 0s";

      
        gameOverText.SetActive(true);

        playerController.enabled = false;

        Destroy(gameObject);
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

    }
}
