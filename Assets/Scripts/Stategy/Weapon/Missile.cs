using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IWeapon
{
    public void Shoot()
    {
        Debug.Log("Súng bắn hỏa tiễn!");
    }
}
