using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float PMHealth;

    public Slider healthSlider;

    public GameManager gameManager;

    private int currentScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        
        //for reg levels - at the start of levels 4, 7, 9 - reset health 
        if (currentScene == 6 || currentScene == 9 || currentScene == 11) gameManager.ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        PMHealth = gameManager.playerHealth;
        healthSlider.value = PMHealth;
        
        //from infinite to infinite death
        if (currentScene == 1 && PMHealth <= 0) SceneManager.LoadScene(2);

        // from RLVL1 (3)... to regular death (14)
        if ((currentScene >= 3 && currentScene <= 12) && PMHealth <= 0) SceneManager.LoadScene(14);


    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartInfinite()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
