using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MinigameUIController : MonoBehaviour
{
    public static MinigameUIController instance;
    public GameObject minigamePrefab, minigame;
    public Text scoreText, timerText;
    public MinigameEndPanelController minigameEndUI;
    public MinigamePetController minigamePetController;
    private int score;
    private float timeRemaining;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one MinigameUIController in the Scene");
    }

    private void OnEnable()
    {
        minigamePetController.enabled = true;
        minigame = Instantiate(minigamePrefab);
        minigame.GetComponent<BaseMinigameController>().Initialize(minigamePetController.transform);
    }

    public virtual void ChangeScore(int amount)
    {
        score += amount;
        UpdateScore(score);
    }

    public void UpdateScore(int score)
    {
        this.score = score;
        if (score >= 3) FinishMinigame(score, timeRemaining);
        scoreText.text = "Score: " + score;
    }

    public void UpdateTimer(float timer)
    {
        timeRemaining = timer;
        timerText.text = string.Format("Time left: {0}", timer);
    }

    public void FinishMinigame(int score, float timeRemaining)
    {
        Destroy(minigame);
        minigameEndUI.gameObject.SetActive(true);
        Camera.main.orthographicSize = 5;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        minigameEndUI.Initialize(score, timeRemaining, timeRemaining > 0);
        minigamePetController.GetComponent<NeedController>().ChangeHappiness(20);
    }

    public void LoseMinigame()
    {
        Destroy(minigame);
        minigamePetController.GetComponent<NeedController>().ChangeHappiness(10);
        minigameEndUI.gameObject.SetActive(true);
        Camera.main.orthographicSize = 5;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        minigameEndUI.Initialize(score, timeRemaining, false);
    }
}
