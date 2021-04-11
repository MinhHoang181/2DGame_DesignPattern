using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IWeapon
{
    public void Shoot()
    {
        Debug.Log("Súng bắn đạn thường!");
    }
}
