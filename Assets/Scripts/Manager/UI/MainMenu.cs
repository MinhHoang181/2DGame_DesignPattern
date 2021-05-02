using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameController.Instance.LoadGameScene();
    }

    public void Option()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
