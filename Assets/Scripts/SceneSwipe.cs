﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwipe : MonoBehaviour
{
    public Animator animator;

    private Vector3 firstTouch;
    private Vector3 lastTouch;

    private bool isBadge;
    private bool isLog;

    //minimum length of swipe for the swipe to register
    private static readonly float lengthOfSwipe = Screen.height * ((float)15.0 / 100);

    void Start()
    {
        isBadge = false;
        isLog = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //for first mouse click
        {
            firstTouch = Input.mousePosition;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            lastTouch = Input.mousePosition;


            if (calculateSwipeDistance() >= lengthOfSwipe && !animator.GetBool("IsOpen"))
            {
                if (isSwipeRight())
                {
                    if (isLog)
                    {
                        ReturnToGame("QuestLog");
                    }
                    else if(!isBadge)
                    {
                        GameToBadge();
                    }
                }
                else
                {
                    if (isBadge)
                    {
                        ReturnToGame("BadgeBox");
                    }
                    else if(!isLog)
                    {
                        GameToLog();
                    }
                }
            }
        }

    }

    //calculating distance between two points on x,y coordinate plane
    private float calculateSwipeDistance()
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(lastTouch.x - firstTouch.x), 2)
            + Mathf.Pow(Mathf.Abs(lastTouch.y - firstTouch.y), 2));
    }

    //compares x values
    private bool isSwipeRight()
    {
        return firstTouch.x < lastTouch.x;
    }

    //goes to badge box
    private void GameToBadge()
    {
        isBadge = true;
        SceneManager.LoadSceneAsync("BadgeBox", LoadSceneMode.Additive);
    }

    //goes to quest log
    private void GameToLog()
    {
        isLog = true;
        SceneManager.LoadSceneAsync("QuestLog", LoadSceneMode.Additive);
    }

    //returns to game screen
    private void ReturnToGame(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        isBadge = false;
        isLog = false;
    }
}