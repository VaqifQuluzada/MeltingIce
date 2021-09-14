using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementController : MonoBehaviour
{
    [SerializeField] private int obstacleSpeed;

    private void Start()
    {
        InvokeRepeating("Move", 0, 0.001f);
    }


    private void Move()
    {
        transform.position += new Vector3(1, 0, 0)*Time.deltaTime*obstacleSpeed;
    }
}
