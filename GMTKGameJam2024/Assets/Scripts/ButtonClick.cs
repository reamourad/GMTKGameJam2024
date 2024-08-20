using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void onStartClick()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void onMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void onTutorialClick()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
