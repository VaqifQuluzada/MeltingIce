using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewPlayerStats",menuName ="ScriptableObjects/NewPlayerStats")]

public class PlayerStats : ScriptableObject
{
    public bool isMusicOn=true;

    public bool isSoundOn=true;

    [SerializeField] private int score;
}
