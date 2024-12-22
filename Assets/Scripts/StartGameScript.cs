using UnityEngine;

public class StartGameScript : MonoBehaviour
{
    public GameObject startScreen;

    void Start()
    {
      
        startScreen.SetActive(true);
       
        Time.timeScale = 0f;
    }

    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
       
            startScreen.SetActive(false);
         
            Time.timeScale = 1f;
        }
    }
}
