using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public enum SlideType
{
    BackThenForward,
    ForwardThenBack
}


public class SlideTrigger : MonoBehaviour {
    
    public bool mustBeHeld = false;

    public SlideType slideType;
    public bool slideActivated = false;
    public bool slideBackActivated = false;
    public bool slideForwardActivated = false;


    public FrameCoreEvent slideBackEvent = new FrameCoreEvent
    {
        eventName = "Slide Back"
    };

    public FrameCoreEvent slideForwardEvent = new FrameCoreEvent
    {
        eventName = "Slide Forward"
    };

    public FrameCoreEvent slideActivatedEvent = new FrameCoreEvent
    {
        eventName = "Slide Activated"
    };

    public FrameCoreEvent slideDeactivatedEvent = new FrameCoreEvent
    {
        eventName = "Slide Deactivated"
    };


    [Title("Slide Settings")]
    /// <summary>
    /// Minimum distance slide will travel on Z axis
    /// </summary>
    public float MinLocalZ = -0.03f;

    /// <summary>
    /// Max distance slide will travel on Z axis
    /// </summary>
    public float MaxLocalZ = 0;

    // Keep track of which way we are sliding
    bool slidingBack = true;

    /// <summary>
    /// Is the Slide locked back due to last shot
    /// </summary>
    public bool LockedBack = false;

    /// <summary>
    /// When true, the slide will be set to 0 mass when not being held. This fixes jitter caused by the slide having a configurable joint attached to the weapon
    /// </summary>
    public bool ZeroMassWhenNotHeld = true;

    [Title("Settings")]
    public Vector3 initialLocalPos;

    //RaycastWeapon parentWeapon;
   /* Grabbable parentGrabbable;
    
    
    Grabbable thisGrabbable;*/
    Rigidbody rigid;
    float initialMass;

    void Start() {
        transform.localPosition = initialLocalPos;

        /*parentGrabbable = transform.parent.GetComponent<Grabbable>();
        thisGrabbable = GetComponent<Grabbable>();*/
        rigid = GetComponent<Rigidbody>();
        initialMass = rigid.mass;

        /*if (parentGrabbable != null) {
            Physics.IgnoreCollision(GetComponent<Collider>(), parentGrabbable.GetComponent<Collider>());
        }*/
    }

    // Update is called once per frame
    void Update() {
        float localZ = transform.localPosition.z;

        if (LockedBack) {
            transform.localPosition = new Vector3(initialLocalPos.x, initialLocalPos.y, MinLocalZ);

            // Not locking back if hand is holding this
            /*if (thisGrabbable != null && thisGrabbable.BeingHeld) {
                UnlockBack();
            }*/
        }

        if (!LockedBack) {
            // Clamp values
            if (localZ <= MinLocalZ) {
                transform.localPosition = new Vector3(initialLocalPos.x, initialLocalPos.y, MinLocalZ);

                if (slidingBack) {
                    onSlideBack();
                }
            }
            else if (localZ >= MaxLocalZ) {
                transform.localPosition = new Vector3(initialLocalPos.x, initialLocalPos.y, MaxLocalZ);

                // Moving forward
                if (!slidingBack) {
                    onSlideForward();
                }
            }
        }

            
    }

    void FixedUpdate() {
        // Change mass of slider rigidbody. This prevents stuttering when the object is not held and the slide is back
        /*if (ZeroMassWhenNotHeld && parentGrabbable.BeingHeld && rigid) {
            rigid.mass = initialMass;
        }
        else if(ZeroMassWhenNotHeld && rigid) {
            // Set mass to very low to prevent stuttering when not held
            rigid.mass = 0.0001f;
        }*/
    }

    public void LockBack() {

        

        if (!LockedBack) {

            /*if (mustBeHeld)
            {
                if (!thisGrabbable.BeingHeld && !parentGrabbable.BeingHeld)
                {
                    return;
                };
            };
*/
            LockedBack = true;
        }
    }

    public void UnlockBack() {


        if (LockedBack) {

            /*if (mustBeHeld)
            {
                if (!thisGrabbable.BeingHeld && !parentGrabbable.BeingHeld)
                {
                    return;
                };
            };
*/
            LockedBack = false;

        }
    }



    //  SLIDE BACK
    void onSlideBack() {

/*        if (thisGrabbable.BeingHeld || parentGrabbable.BeingHeld) {
            playSoundInterval(0, 0.2f, 0.9f);
        }*/


        if (slideType == SlideType.BackThenForward)
        {

            slideBackActivated = true;
            slideForwardActivated = false;

        } else if (slideType == SlideType.ForwardThenBack)
        {
            if (slideForwardActivated)
            {
                slideBackActivated = true;
            }
            else {
                slideBackActivated = false;
                slideForwardActivated = false;
            };
        };

        slideBackEvent.Activate();

        CheckActivated();

        slidingBack = false;
    }




    //  SLIDE FORWARD
    void onSlideForward() {

/*        if (thisGrabbable.BeingHeld || parentGrabbable.BeingHeld) {
            playSoundInterval(0.2f, 0.35f, 1f);
        }*/

        if (slideType == SlideType.ForwardThenBack)
        {

            slideForwardActivated = true;
            slideBackActivated = false;

        }
        else if (slideType == SlideType.BackThenForward)
        {
            if (slideBackActivated)
            {
                slideForwardActivated = true;
            }
            else
            {
                slideBackActivated = false;
                slideForwardActivated = false;
            };
        };

        slideForwardEvent.Activate();

        CheckActivated();


        slidingBack = true;
    }






    //  CHECK IF BOTH SLIDE DIRECTIONS ARE ACTIVATED
    void CheckActivated()
    {
        if (!slideActivated && (slideBackActivated && slideForwardActivated))
        {
            slideActivatedEvent.Activate();
        }
        else if (slideActivated && !(slideBackActivated && slideForwardActivated))
        {
            slideDeactivatedEvent.Activate();
        };

        slideActivated = (slideBackActivated && slideForwardActivated);
    }



    public void ResetTrigger()
    {
        slideBackActivated = false;
        slideForwardActivated = false;
        slideActivated = false;
    }

/*    void playSoundInterval(float fromSeconds, float toSeconds, float volume) {
        if (audioSource) {

            if (audioSource.isPlaying) {
                audioSource.Stop();
            }

            audioSource.pitch = Time.timeScale;
            audioSource.time = fromSeconds;
            audioSource.volume = volume;
            audioSource.Play();
            audioSource.SetScheduledEndTime(AudioSettings.dspTime + (toSeconds - fromSeconds));
        }
    }*/
}

