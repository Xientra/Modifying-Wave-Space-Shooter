using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject gameUICanvas;
    [SerializeField]
    private TMPro.TextMeshProUGUI highscore;
    
#pragma warning restore 0649

    private void Start()
    {
        Player.Instance.onDeath += switchCanvas;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Close")) {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }
    }

    private void switchCanvas()
    {
        gameUICanvas.SetActive(false);
        showScore();
        gameOverCanvas.SetActive(true);
    }

    private void showScore() 
    {
        highscore.text = "Your score: " + WaveController.Instance.Score;
    }
}
