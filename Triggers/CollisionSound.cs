using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CollisionSound : MonoBehaviour {

    [TitleGroup("Settings")]
    public LayerMaskNames layerMask = LayerMaskNames.CollisionSounds;

    [TitleGroup("Ignore Collision")]
    public IgnoreCollision ignoreCollisions;

    [TitleGroup("Sound Controller")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    public SoundController soundController;

    [TitleGroup("Volume")]
    /// <summary>
    /// Cap volume at this level 0 - 1
    /// </summary>
    public MaxMinFloat volumeRange = new MaxMinFloat
    {
        min = 0.25f,
        max = 1f
    };
    [TitleGroup("Volume")]
    public float beingHeldVolume = 0.1f;


    [FoldoutGroup("Parts")]
    public Collider col;



    [FoldoutGroup("System")]
    public float LastRelativeVelocity = 0;
    [FoldoutGroup("System")]
    public float initTime = 0f;



    void Start() {

        initTime = Time.time;

        if (!col)
        {
            col = GetComponent<Collider>();
        };


        
    }

    private void OnCollisionEnter(Collision collision) {


        GameObject collisionObject = collision.contacts[0].thisCollider.gameObject;

        if (!ignoreCollisions.CanCollide(collisionObject))
        {
            return;
        };



        if (!Frame.core.layerMasks.InLayerMask(layerMask, collisionObject))
        {
            return;
        };

        // Just spawned, don't fire collision sound immediately
        if (Time.time - initTime < 0.2f) {
            return;
        }

        // No Collider present, don't play sound
        if (col == null || !col.enabled) {
            return;
        }

        float colVelocity = collision.relativeVelocity.magnitude;

        bool otherColliderPlayedSound = false;
        CollisionSound colSound = collision.collider.GetComponent<CollisionSound>();

        // Don't play a sound if something else is playing the same sound.
        // This prevents overlap
        Debug.LogError("Needs A Debounce...");
        /*if (colSound) {
            otherColliderPlayedSound = colSound.soundController.IsCoolingDown() && colSound.soundController.currentAudioClip == soundController.currentAudioClip;
        };*/

        float soundVolume = Mathf.Clamp(collision.relativeVelocity.magnitude / 10, volumeRange.min, volumeRange.max);
        bool minVelReached = colVelocity > 0.1f;

        // If object is being held play the sound very lightly
        //
        //  HHHHHHEEEEEEY
        //
        /*if(!minVelReached && grab != null && grab.BeingHeld) {
            minVelReached = true;
            soundVolume = beingHeldVolume;
        }*/

        if (minVelReached && !otherColliderPlayedSound) {

            LastRelativeVelocity = colVelocity;

            Debug.LogError("Needs A SoundFX...");

           

        }
    }

}
