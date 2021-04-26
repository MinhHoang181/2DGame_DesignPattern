using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Piston", menuName = "Scriptable Object/Weapons/Piston")]
public class PistonScriptableObject : WeaponScriptableObject
{
    public GameObject bullet;
    public int ammoQuantity;
}
