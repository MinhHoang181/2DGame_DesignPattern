using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableCharacter : ScriptableObject
{
    public Sprite characterSprite;
    public CharacterType characterType;
    public string characterName;
    public int health;
    public int speed;
    public float timeStun;

    public ScriptableCharacter(CharacterType characterType)
    {
        this.characterType = characterType;
    }
}
