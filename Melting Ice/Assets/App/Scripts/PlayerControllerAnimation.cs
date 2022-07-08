using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAnimation : MonoBehaviour
{
    public static PlayerControllerAnimation instance;

    [SerializeField] private float moveSpeed = 10;

    [SerializeField] private float jumpPower = 100;

    [SerializeField] private Animation playerAnimation;

    [SerializeField] private AudioSource playerMovementSFXSource;

    bool swiping = false;

    SwipeDirection currentDirection = SwipeDirection.MID;

    

    [SerializeField] private AnimationClip[] midToLeftClips;

    [SerializeField] private AnimationClip[] midToRightClips;

    [SerializeField] private AnimationClip[] rightToMidClips;

    [SerializeField] private AnimationClip[] leftToMidClips;

    [SerializeField] private AnimationClip[] leftJumps;

    [SerializeField] private AnimationClip[] midJumps;

    [SerializeField] private AnimationClip[] rightJumps;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }

        instance = this;

        DontDestroyOnLoad(this);
    }

    public void Swipe(SwipeDirection direction)
    {
        if (swiping)
        {
            return;
        }

        int randomAnimationIndex = 0;
        //reference to current played clip
        AnimationClip currentPlayedClip=null;



        ////////////For mid direction actions//////////
        if (currentDirection == SwipeDirection.MID)
        {
            if (direction == SwipeDirection.LEFT)
            {
                currentDirection = SwipeDirection.LEFT;

                randomAnimationIndex = Random.Range(0, midToLeftClips.Length);

                currentPlayedClip = midToLeftClips[randomAnimationIndex];

            }


            else if(direction==SwipeDirection.RIGHT)
            {
                currentDirection = SwipeDirection.RIGHT;

                randomAnimationIndex = Random.Range(0, midToRightClips.Length);

                currentPlayedClip = midToRightClips[randomAnimationIndex];

            }

            else if (direction == SwipeDirection.UP)
            {
                randomAnimationIndex = Random.Range(0, midJumps.Length);

                currentPlayedClip = midJumps[randomAnimationIndex];
            }
        }


        ////////////For left direction actions//////////
        else if (currentDirection == SwipeDirection.LEFT)
        {
            if (direction == SwipeDirection.RIGHT)
            {
                currentDirection = SwipeDirection.MID;

                randomAnimationIndex = Random.Range(0, leftToMidClips.Length);

                currentPlayedClip = leftToMidClips[randomAnimationIndex];
            }

            else if (direction == SwipeDirection.UP)
            {
                randomAnimationIndex = Random.Range(0, leftJumps.Length);

                currentPlayedClip = leftJumps[randomAnimationIndex];
            }
        }


        ////////////For right direction actions//////////
        else if (currentDirection == SwipeDirection.RIGHT)
        {
            if (direction == SwipeDirection.LEFT)
            {
                currentDirection = SwipeDirection.MID;

                randomAnimationIndex = Random.Range(0, rightToMidClips.Length);

                currentPlayedClip = rightToMidClips[randomAnimationIndex];
            }

            else if (direction == SwipeDirection.UP)
            {
                randomAnimationIndex = Random.Range(0, rightJumps.Length);

                currentPlayedClip = rightJumps[randomAnimationIndex];
            }
        }

        if (currentPlayedClip != null)
        {
            PlayAnimation(currentPlayedClip);
        }

    }

    private void PlayAnimation(AnimationClip clip)
    {
        playerAnimation.clip = clip;

        playerAnimation.Stop();
        playerAnimation.Play();
    }

    private void PlayMovementSFX(AudioClip audioClip)
    {
        playerMovementSFXSource.clip = audioClip;

        playerMovementSFXSource.Stop();

        playerMovementSFXSource.Play();
    }
    //IEnumerator SwipeMove(Vector3 targetPosition)
    //{
    //    swiping = true;

    //    while (transform.position != targetPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    //        yield return new WaitForEndOfFrame();
    //    }

    //    swiping = false;
    //    StopAllCoroutines();
    //}

    //IEnumerator Jump()
    //{
    //    swiping = true;
    //    Vector3 targetUpPosition = new Vector3(transform.position.x, 4, transform.position.z);

    //    Vector3 targetDownPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);

    //    while (transform.position != targetUpPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, targetUpPosition, Time.deltaTime * moveSpeed);
    //        yield return new WaitForEndOfFrame();
    //    }

    //    while (transform.position != targetDownPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, targetDownPosition, Time.deltaTime * moveSpeed);
    //        yield return new WaitForEndOfFrame();
    //    }
            
    //    swiping = false;
    //    StopAllCoroutines();
    //}
}