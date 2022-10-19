using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public bool startAtOffset = false;

    // camera will follow this object
    public Transform target;

    public float smoothSpeed = 0.125f;
    public bool clampY;
    public MaxMinFloat offsetClampY;    
    public Vector3 offset;


    public Transform cameraGimbal;

    public Vector2 distanceFromCameraCenter;
    public Vector2 rotationMultipler;
    public Vector2 maxRotation;

    public float smoothRotationSpeed = 0.125f;

    private void Start()
    {


        if(startAtOffset)
        {
            transform.position = target.position + offset;
        };
    }

    private void FixedUpdate()
    {

        Vector3 desiredPosition = target.position + offset;
        
        if (desiredPosition.y > offsetClampY.max)
        {
            desiredPosition.y = (offsetClampY.max) + (desiredPosition.y - offsetClampY.max) / 5f;
        };

        //desiredPosition.y = Mathf.Clamp(desiredPosition.y, offsetClampY.min, offsetClampY.max);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (smoothSpeed *10f) * Time.deltaTime);

        transform.position = smoothedPosition;

        distanceFromCameraCenter.x = transform.position.x - target.position.x;
        distanceFromCameraCenter.y = transform.position.z - target.position.z;

        Vector3 newRotation = new Vector3(  (distanceFromCameraCenter.y * rotationMultipler.y) , 0f, (distanceFromCameraCenter.x * rotationMultipler.x));

        newRotation.x = Mathf.Clamp(newRotation.x, -maxRotation.x, maxRotation.x);
        newRotation.z = Mathf.Clamp(newRotation.z, -maxRotation.y, maxRotation.y);

        cameraGimbal.eulerAngles = newRotation;

    }
}
