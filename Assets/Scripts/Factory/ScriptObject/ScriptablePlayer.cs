using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Object/Characters/Player")]
public class ScriptablePlayer : ScriptableCharacter
{
    [Header("Player")]
    public ScriptableWeapon weapon;
}
