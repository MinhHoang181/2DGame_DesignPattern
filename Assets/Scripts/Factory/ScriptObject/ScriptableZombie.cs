using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "Scriptable Object/Characters/Zombie")]
public class ScriptableZombie : ScriptableCharacter
{
    [Header("Zombie")]
    public int damage;
    public float timeToAttack;
    public float pushBackStrength;
}
