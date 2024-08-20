using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] TMP_Text waveDisplay;
    public static int turnNumber = 0; 
    public void onStartClick()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void onMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Start()
    {
        waveDisplay.text = "Wave: " + turnNumber; 
    }
}
