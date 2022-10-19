using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_RotateToFrom : MonoBehaviour
{
    public bool rotating = false;
    public bool direction = true;
    public Axis axis;
    public Transform rotationPoint;
    public MaxMinFloat fromTo;
    public float speed = 1f;
    

    public void RotateActivateForwards()
    {
        direction = true;
        rotating = true;
    }

    public void RotateActivateBackwards()
    {
        direction = false;
        rotating = true;
    }


    void RotateAxis ()
    {
        Vector3 rotation = rotationPoint.localEulerAngles;

        float rotationResult = 0f;

        if (axis == Axis.x)
        {
            rotation.x = Mathf.Clamp(rotation.x + (speed * (direction ? 1 : -1)), fromTo.min, fromTo.max);
            rotationResult = rotation.x;
        }
        else if (axis == Axis.y)
        {
            rotation.y = Mathf.Clamp(rotation.y + (speed * (direction ? 1 : -1)), fromTo.min, fromTo.max);
            rotationResult = rotation.y;
        }
        else if (axis == Axis.z)
        {
            rotation.z = Mathf.Clamp(rotation.z + (speed * (direction ? 1 : -1)), fromTo.min, fromTo.max);
            rotationResult = rotation.z;
        };

        if (direction && rotationResult == fromTo.max ||
            !direction && rotationResult == fromTo.min)
        {
            rotating = false;
        };

        rotationPoint.localEulerAngles = rotation;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            RotateAxis();
        };
    }
}
