using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Follower : MonoBehaviour
{
    [Title("Settings")]
    public UpdateType update;

    [Title("Follower")]
    public Transform follower;

    [Title("Target")]
    public Transform target;


    [Title("Start")]
    public bool started = false;
    public bool startAtTargetPosition = false;
    public bool startAtTargetRotation = false;

    [Title("Position")]
    public bool followPosition = true;
    public bool smoothPosition = false;
    public float positionSpeed = 1f;
    [FoldoutGroup("Position Settings")]
    public Vector3 positionOffset = Vector3.zero;
    [FoldoutGroup("Position Lock")]
    public bool lockPositionX = false;
    [FoldoutGroup("Position Lock")] 
    public bool lockPositionY = false;
    [FoldoutGroup("Position Lock")] 
    public bool lockPositionZ = false;


    [Title("Rotation")]
    public bool followRotation = true;
    public bool smoothRotation = false;
    public float rotationSpeed = 1f;
    [FoldoutGroup("Rotation Lock")]
    public bool lockRotationX = false;
    [FoldoutGroup("Rotation Lock")]
    public bool lockRotationY = false;
    [FoldoutGroup("Rotation Lock")]
    public bool lockRotationZ = false;


    // Start is called before the first frame update
    void Start()
    {
        if (!target)
        {
            return;
        };
    }

    public void Init()
    {
        if (startAtTargetPosition)
        {
            follower.position = target.position;
        };
        if (startAtTargetRotation)
        {
            follower.rotation = target.rotation;
        };
    }

    private void FixedUpdate()
    {
        if (update == UpdateType.FixedUpdate)
        {
            Follow();
        };
    }

    private void Update()
    {
        if (update == UpdateType.Update)
        {
            Follow();
        };
    }

    private void LateUpdate()
    {
        if (update == UpdateType.LateUpdate)
        {
            Follow();
        };
    }


    void Follow()
    {
        if (!target)
        {
            return;
        };

        if (followPosition)
        {
            Vector3 currentPosition = follower.position;
            Vector3 newPosition = target.position + positionOffset;

            if (lockPositionX)
            {
                newPosition.x = currentPosition.x;
            };
            if (lockPositionY)
            {
                newPosition.y = currentPosition.y;
            };
            if (lockPositionZ)
            {
                newPosition.z = currentPosition.z;
            };


            if (smoothPosition)
            {
                follower.position = Vector3.Lerp(follower.position, newPosition, positionSpeed * Time.deltaTime);
            }
            else {
                follower.position = newPosition;
            };

        };

        if (followRotation)
        {
            Vector3 currentRotation = follower.eulerAngles;
            Vector3 newRotation = target.eulerAngles;

            if (lockRotationX)
            {
                newRotation.x = currentRotation.x;
            };
            if (lockRotationY)
            {
                newRotation.y = currentRotation.y;
            };
            if (lockRotationZ)
            {
                newRotation.z = currentRotation.z;
            };


            if (smoothRotation)
            {
                follower.rotation = Quaternion.Lerp(follower.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
            }
            else
            {
                follower.rotation = Quaternion.Euler(newRotation);
            };

        };

    }



}
