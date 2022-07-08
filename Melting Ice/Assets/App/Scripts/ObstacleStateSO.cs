using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ObstacleState",menuName ="ScriptableObjects/NewObstacleState")]
public class ObstacleStateSO : ScriptableObject
{
    [Header("Up,Right,Left")]
    public StateArrayElement[] stateArrayElements;
}

[System.Serializable]
public class StateArrayElement
{
    public GameObject[] stateArrayElement;
}
