using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : UIManager
{
    public void OnClickStart()
    {
        PlayClickSound();           
        LoadScene("StageScene");
    }
    public void OnClickExit()
    {
        PlayClickSound(); 
        QuitGame();      
    }
}
