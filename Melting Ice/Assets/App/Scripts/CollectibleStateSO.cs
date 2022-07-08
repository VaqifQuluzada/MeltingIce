using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleState", menuName = "ScriptableObjects/NewCollectibleState")]

public class CollectibleStateSO : ScriptableObject
{
    [Header("Up,Right,Left")]
    public StateArrayElement[] stateArrayElements;
}
