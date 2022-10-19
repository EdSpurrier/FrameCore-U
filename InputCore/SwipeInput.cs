using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    [Title("Settings")]
    public float swipeDistance = 0.5f;
    public float maxSwipeTime = 0.2f;


    [FoldoutGroup("Events")]
    [HideLabel]
    public FrameCoreEvent swipeUpEvent = new FrameCoreEvent { 
        eventName = "Swipe Up"
    };

    [FoldoutGroup("Events")]
    [HideLabel]
    public FrameCoreEvent swipeRightEvent = new FrameCoreEvent
    {
        eventName = "Swipe Right"
    };

    [FoldoutGroup("Events")]
    [HideLabel]
    public FrameCoreEvent swipeDownEvent = new FrameCoreEvent
    {
        eventName = "Swipe Down"
    };

    [FoldoutGroup("Events")]
    [HideLabel]
    public FrameCoreEvent swipeLeftEvent = new FrameCoreEvent
    {
        eventName = "Swipe Left"
    };



    [Title("System")]
    public float currentSwipeTime = 0f;

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void GetDirection()
    {
        if (currentSwipeTime > maxSwipeTime)
        {
            return;
        };

        //swipe upwards
        if (currentSwipe.y > 0 && currentSwipe.x > -swipeDistance && currentSwipe.x < swipeDistance)
        {
            Debug.Log("up swipe");
            swipeUpEvent.Activate();
        }
        //swipe down
        if (currentSwipe.y < 0 && currentSwipe.x > -swipeDistance && currentSwipe.x < swipeDistance)
        {
            Debug.Log("down swipe");
            swipeDownEvent.Activate();
        }
        //swipe left
        if (currentSwipe.x < 0 && currentSwipe.y > -swipeDistance && currentSwipe.y < swipeDistance)
        {
            Debug.Log("left swipe");
            swipeLeftEvent.Activate();
        }
        //swipe right
        if (currentSwipe.x > 0 && currentSwipe.y > -swipeDistance && currentSwipe.y < swipeDistance)
        {
            Debug.Log("right swipe");
            swipeRightEvent.Activate();
        };
    }

    private void Update()
    {
        Swipe();
    }


    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);

                currentSwipeTime = 0f;

            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                GetDirection();

            };

        }
        else if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            currentSwipeTime = 0f;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            GetDirection();
        }
        else {
            currentSwipeTime += Time.deltaTime;
        };
    }

}
