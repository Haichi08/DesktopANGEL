using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameEndPanelController : MonoBehaviour
{
    public Text resultText, titleText;

    public void Initialize(int score, float timeRemaining, bool victory)
    {
        resultText.text =
            string.Format("You obtained {0} points, and had {1} seconds left", score, timeRemaining);
        titleText.text = victory ? "You won!" : "You lost!";
    }

    private void OnDisable()
    {
        MinigameUIController.instance.minigamePetController.enabled = false;
    }
}

