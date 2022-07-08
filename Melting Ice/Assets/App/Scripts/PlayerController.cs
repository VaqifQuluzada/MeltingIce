using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float moveSpeed = 10;

    [SerializeField] private float jumpPower = 100;
    
    bool swiping = false;

    SwipeDirection currentDirection = SwipeDirection.MID;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }

        instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {


    }

    public void Swipe(SwipeDirection direction)
    {
        if (swiping)
        {
            return;
        }


        if (direction == SwipeDirection.UP)
        {
            StartCoroutine(Jump());
            return;
        }

        if (currentDirection == SwipeDirection.MID)
        {
            if (direction == SwipeDirection.LEFT)
            {
                currentDirection = SwipeDirection.LEFT;

                //
                StartCoroutine(SwipeMove(new Vector3(1.5f, 0.5f, -22)));
            }
            else
            {
                currentDirection = SwipeDirection.RIGHT;
                //
                StartCoroutine(SwipeMove(new Vector3(-1.5f, 0.5f, -22)));
            }
        }

        else if (currentDirection == SwipeDirection.LEFT)
        {
            if (direction == SwipeDirection.RIGHT)
            {
                currentDirection = SwipeDirection.MID;
                StartCoroutine(SwipeMove(new Vector3(0, 0.5f, -22)));
            }
        }

        else if (currentDirection == SwipeDirection.RIGHT)
        {
            if (direction == SwipeDirection.LEFT)
            {
                currentDirection = SwipeDirection.MID;
                //
                StartCoroutine(SwipeMove(new Vector3(0, 0.5f, -22)));
            }
        }
        
    }


    IEnumerator SwipeMove(Vector3 targetPosition)
    {
        swiping = true;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        swiping = false;
        StopAllCoroutines();
    }

    IEnumerator Jump()
    {
        swiping = true;
        Vector3 targetUpPosition = new Vector3(transform.position.x, 4, transform.position.z);

        Vector3 targetDownPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);

        while (transform.position != targetUpPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetUpPosition, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        while (transform.position != targetDownPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDownPosition, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }
            
        swiping = false;
        StopAllCoroutines();
    }
}

public enum SwipeDirection { MID,LEFT, RIGHT,UP};