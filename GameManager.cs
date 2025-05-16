using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float playerHealth;
    public float maxHealth = 100f;

    public GameObject screen;
    public Animator animator;

    [Header("Display Kill Count")]
    public TextMeshProUGUI displayKillCount;
    public GhostKillCounter GhostKillCounter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = maxHealth;       

        StartCoroutine(FadeAfterDelay());

        displayKillCount.text = "Ghosts Killed: " + GhostKillCounter.ghostKilled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeAfterDelay()
    {
        yield return new WaitForSeconds(5);
        animator.SetTrigger("fade");
        yield return new WaitForSeconds(2);
        screen.gameObject.SetActive(false);
    }

    public void ChangeHealth(float damage)
    {
        this.playerHealth -= damage;
        return;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetHealth()
    {
        playerHealth = maxHealth;
    }

    public void RestartInfinite()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTC1()
    {
        SceneManager.LoadScene(15);
    }

    public void LoadTC2()
    {
        SceneManager.LoadScene(16);
    }

    public void LoadTC3()
    {
        SceneManager.LoadScene(17);
    }

    public void LoadRegular()
    {
        SceneManager.LoadScene(3);
    }
}
