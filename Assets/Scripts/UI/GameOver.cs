using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : UIManager
{
    public void OnClickRetry()
    {
        PlayClickSound();
        SceneManager.LoadScene("StageScene");
    }
    public void OnClickTitle()
    {
        PlayClickSound();
        SceneManager.LoadScene("Title Scene");
    }
}