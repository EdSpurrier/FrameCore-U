using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestCamera : MonoBehaviour
{
    
    public Transform movePointTarget;
    public Transform lookPointTarget;

    public float smoothRotationSpeed = 0.125f;
    public float smoothMoveSpeed = 0.125f;

    public Transform cameraActual;

    public bool followPointOfInterest = false;

    public Vector3 cameraNormalRotation;

    private void FixedUpdate()
    {


        if (followPointOfInterest)
        {
            Vector3 desiredPosition = movePointTarget.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (smoothMoveSpeed * 10f) * Time.deltaTime);

            transform.position = smoothedPosition;

            var targetRotation = Quaternion.LookRotation(lookPointTarget.position - cameraActual.position);

            // Smoothly rotate towards the target point.
            cameraActual.rotation = Quaternion.Slerp(cameraActual.rotation, targetRotation, smoothRotationSpeed * Time.deltaTime);
        }
        else {
            if (Vector3.Distance(cameraActual.localEulerAngles, cameraNormalRotation) > 1f)
            {
                //  RETURN TO NORMAL
                cameraActual.localEulerAngles = Vector3.Lerp(cameraActual.localEulerAngles, cameraNormalRotation, smoothRotationSpeed * Time.deltaTime);
            };
        }


        



    }
}
