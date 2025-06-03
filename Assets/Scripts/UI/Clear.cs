using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : UIManager
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        int score = GameManager.Instance.StageScore;
        int highScore = GameManager.Instance.HighScore;

        if (score > highScore)
        {
            GameManager.Instance.HighScore = score;
            highScore = score;
        }

        scoreText.text = $"{score}";
        highScoreText.text = $"{highScore}";
    }

    public void OnClickTitle()
    {
        PlayClickSound();
        SceneManager.LoadScene("Title Scene");
    }
}
