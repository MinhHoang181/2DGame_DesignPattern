using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] string defaultMap;
    public void PlayGame()
    {
        SceneManager.LoadScene(defaultMap);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}
