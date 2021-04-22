using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        if (GameController.Instance.Player != null)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 playerPosition = GameController.Instance.Player.transform.position;
        transform.position = playerPosition + Vector3.back;
    }
}
