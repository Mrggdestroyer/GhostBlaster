using System;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public bool stopwatchActive = true;

    public float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive) currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("0.00");
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }
}