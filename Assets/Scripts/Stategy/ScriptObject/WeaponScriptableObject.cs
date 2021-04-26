using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Strategy;

public class WeaponScriptableObject : ScriptableObject
{
    public Sprite weaponSprite;
    public GameObject bullet;

    public string weaponName;
    public WeaponType type;
    public int damage;
    public int pushBackStrength;
    public float distance;
    public float fireDelay;
}
