using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class DieMenu : MonoBehaviour
{
    public void Restart()
    {
        GameController.Instance.LoadGameScene();
    }

    public void Exit()
    {
        GameController.Instance.LoadMainScene();
    }
}
