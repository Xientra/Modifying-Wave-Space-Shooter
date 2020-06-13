using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject gameUICanvas;
    [SerializeField]
    private TMPro.TextMeshProUGUI highscore;

    private void Start()
    {
        Player.Instance.onDeath += switchCanvas;
    }

    private void switchCanvas()
    {
        inGameUI.SetActive(false);
        showScore();
        gameOverCanvas.SetActive(true);
    }

    private void showScore() 
    {
        highscore.text = "Your score: " + WaveController.Instance.Score;
    }
}
