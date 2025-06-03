using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;
    protected void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    protected void QuitGame()
    {
        Application.Quit();
    }

    protected void PlayClickSound()
    {
        GameManager.Instance.Audio.PlaySFX(clickClip);
    }
}
