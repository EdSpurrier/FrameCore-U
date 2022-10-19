using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Touch,
    Keyboard,
    Gyro
}



public class InputCore : MonoBehaviour
{
    public bool active = false;

    public Vector2 input;
    public bool jump = false;



    public InputType inputType;

    

    public void SetTouchInputActive()
    {
        inputType = InputType.Touch;
    }

    public void Init()
    {
        active = true;
    }

    private void Start()
    {

        Init();
    }


    public void CheckKeyboardInput()
    {
        if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Horizontal") < 0f || Input.GetButtonDown("Jump"))
        {
            inputType = InputType.Keyboard;
        }
        else {
            input = Vector2.zero;
        };
    }

    public Vector2 GetInputAxis()
    {
        if (!active)
        {
            return Vector2.zero;
        };

        jump = false;

        CheckKeyboardInput();


        if (inputType == InputType.Keyboard)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            jump = Input.GetButtonDown("Jump");
        };
        

        return input;
    }



    public bool GetInputJump()
    {
        if (!active)
        {
            return false;
        };

        jump = Input.GetButtonDown("Jump");

        return jump;
    }
}
