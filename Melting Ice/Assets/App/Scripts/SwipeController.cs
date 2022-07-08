using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    ////reference to the player controller
    //[SerializeField] private PlayerController playerController;

    [SerializeField] private PlayerControllerAnimation playerControllerAnimation;

    //start position where touch starts.
    [SerializeField] private Vector2 startPos;

    //max number of pixels for swipe effect.
    [SerializeField] private int pixelDistanceToSwipe;

    //for checking if player touched the screen.
    [SerializeField] private bool fingerDown=false;

    private void Start()
    {
        //if (playerController == null)
        //{
        //    playerController = PlayerController.instance;
        //}

        if (playerControllerAnimation == null)
        {
            playerControllerAnimation = PlayerControllerAnimation.instance;
        }
    }

    private void Update()
    {
        if (GamePlayManager.instance.gamePlayState != GamePlayStates.Gaming)
        {
            return;
        }

#if UNITY_EDITOR
        if (fingerDown == false && Input.GetMouseButtonDown(0))
        {
            //we get the start pos as the touch's position.
            startPos = Input.mousePosition;

            fingerDown = true;
        }

        if (fingerDown == true)
        {
            if (Input.mousePosition.y > startPos.y + pixelDistanceToSwipe)
            {
                //Debug.Log("Swiped up");

                //playerController.Swipe(SwipeDirection.UP);

                playerControllerAnimation.Swipe(SwipeDirection.UP);

                fingerDown = false;
            }
            else if (Input.mousePosition.y < startPos.y - pixelDistanceToSwipe)
            {
                //Debug.Log("Swiped down");
                fingerDown = false;
            }
            else if (Input.mousePosition.x > startPos.x + pixelDistanceToSwipe)
            {
                //Debug.Log("Swiped right");

                //playerController.Swipe(SwipeDirection.RIGHT);

                playerControllerAnimation.Swipe(SwipeDirection.RIGHT);

                fingerDown = false;
            }
            else if (Input.mousePosition.x < startPos.x - pixelDistanceToSwipe)
            {
                //Debug.Log("Swiped left");

                //playerController.Swipe(SwipeDirection.LEFT);

                playerControllerAnimation.Swipe(SwipeDirection.LEFT);

                fingerDown = false;
            }


            if (Input.GetMouseButtonUp(0))
            {
                fingerDown = false;
            }
        }


#endif

#if UNITY_ANDROID || UNITY_IOS
        //if player didnt touch the screen, and already touched-first touch, and the touch is the began phrase
        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            //we get the start pos as the touch's position.
            startPos = Input.touches[0].position;

            fingerDown = true;
        }

        if (fingerDown == true)
        {
            if (Input.touches[0].position.y > startPos.y + pixelDistanceToSwipe)
            {
                Debug.Log("Swiped up");

                playerControllerAnimation.Swipe(SwipeDirection.UP);

                fingerDown = false;
            }
            else if (Input.touches[0].position.y < startPos.y - pixelDistanceToSwipe)
            {
                Debug.Log("Swiped down");

                fingerDown = false;

            }
            else if (Input.touches[0].position.x > startPos.x + pixelDistanceToSwipe)
            {
                Debug.Log("Swiped right");

                playerControllerAnimation.Swipe(SwipeDirection.RIGHT);

                fingerDown = false;
            }
            else if (Input.touches[0].position.x < startPos.x - pixelDistanceToSwipe)
            {
                Debug.Log("Swiped left");

                playerControllerAnimation.Swipe(SwipeDirection.LEFT);

                fingerDown = false;
            }

            if (fingerDown && Input.touches[0].phase==TouchPhase.Ended)
            {
                fingerDown = false;
            }
        }
#endif
    }

}
