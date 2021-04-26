using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Strategy;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/Weapons/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public Sprite weaponSprite;

    public string weaponName;
    public WeaponType type;
    public int damage;
    public float pushBackStrength;
    public float distance;
    public float fireDelay;
}
