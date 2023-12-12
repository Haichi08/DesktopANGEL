using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMinigameController : MonoBehaviour
{
    protected int score;
    public float timer, cameraSize;
    public Transform[] startingPositions;

    private void Start()
    {
        if (timer == 0)
        {
            ChangeTimer(30);
        }
    }

    public void Initialize(Transform pet)
    {
        Camera.main.orthographicSize = cameraSize;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        transform.position = Vector3.zero;
        pet.position = startingPositions[Random.Range(0, startingPositions.Length)].position;
    }


    protected virtual void ChangeTimer(float change)
    {
        timer += change;
        if (MinigameUIController.instance == null) return;
        MinigameUIController.instance.UpdateTimer(timer);
    }

    protected virtual void Update()
    {
        ChangeTimer(-Time.deltaTime);
        if (timer < 0)
        {
            MinigameUIController.instance.FinishMinigame(score, timer);
            Destroy(gameObject, 1);
        }
    }

    protected virtual void GoalReached()
    {
        if (MinigameUIController.instance == null) return;
        MinigameUIController.instance.FinishMinigame(score, timer);
        Camera.main.orthographicSize = 5;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Destroy(gameObject);
    }
}
