using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        GameController.Instance.ChangeState();
    }

    public void Option()
    {

    }

    public void Exit()
    {
        GameController.Instance.LoadMainScene();
    }
}
