using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Deform;

public class Deform_Bend : MonoBehaviour
{
    [Title("Animation Curves")]
    public AnimationCurve angleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float GraphTimeMultiplier = 1, GraphIntensityMultiplier = 1;

    [Title("Settings")]
    public AnimationLoopMode loopMode = AnimationLoopMode.PingPong;

    [Title("System")]
    public Direction direction = Direction.Forward;
    public float time = 0f;

    [Title("Parts")]
    public BendDeformer bendDeformer;

    private bool canUpdate;
    private float startTime;


    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
        direction = Direction.Forward;

        if (!bendDeformer)
        {
            bendDeformer = GetComponent<BendDeformer>();
        };

        bendDeformer.Angle = angleCurve.Evaluate(0);
    }


    private void Update()
    {
        if (direction == Direction.Forward)
        {
            time = Time.time - startTime;
        }
        else if (direction == Direction.Backward)
        {
            time = GraphTimeMultiplier - (Time.time - startTime);
        };
        
        

        if (canUpdate)
        {
            var eval = angleCurve.Evaluate(time / GraphTimeMultiplier) * GraphIntensityMultiplier;
            bendDeformer.Angle = eval;
        };


        if (time >= GraphTimeMultiplier)
        {
            //  IF ONCE MODE THEN STOP AND DISABLE THIS
            if (loopMode == AnimationLoopMode.Once)
            {
                canUpdate = false;
                this.enabled = false;
                return;
            } else if (loopMode == AnimationLoopMode.Loop)
            {
                startTime = Time.time;
            }
            else if (loopMode == AnimationLoopMode.PingPong)
            {
                startTime = Time.time;

                direction = Direction.Backward;
            };




        } else if (time <= 0f && direction == Direction.Backward)
        {
            startTime = Time.time;
            direction = Direction.Forward;
        };
    }
}
