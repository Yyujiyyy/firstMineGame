﻿using UnityEngine;
using TMPro;

public class CountUpTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay(elapsedTime);
        }

        
    }

    private void UpdateTimerDisplay(float time)
    {
        if (this.gameObject.activeSelf == true)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            //int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay(elapsedTime);
        isRunning = true;
    }

    public void StopTimer() => isRunning = false;           //書き方の理解
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay(elapsedTime);
    }
}