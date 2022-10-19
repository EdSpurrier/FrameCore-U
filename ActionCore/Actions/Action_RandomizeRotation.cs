using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_RandomizeRotation
{
    [HorizontalGroup("Split")]
    [VerticalGroup("Split/Left")]
    [HideLabel]
    public Axis axis;

    [HideLabel]
    [VerticalGroup("Split/Left")]
    public RotationType rotationType;

    [VerticalGroup("Split/Right")]
    [HideLabel]
    public MaxMinInt randomRange;

    public List<Transform> transforms;

    [Title("System")]
    public DeBugger debug;
    


    public void Activate()
    {
        foreach(Transform trans in transforms)
        {
            RotationOfTransform(trans);
        };
    }

    public void RotationOfTransform(Transform trans)
    {


        Vector3 rotation = Vector3.zero;
        if (rotationType == RotationType.LocalRotation)
        {
            rotation = trans.localEulerAngles;
        } else if (rotationType == RotationType.Rotation) {
            rotation = trans.eulerAngles;
        };


        RandomSeedGenerator.RandomizeSeed();

        if (axis == Axis.x)
        {
            rotation.x = Random.Range(randomRange.min, randomRange.max);
        } else if (axis == Axis.y)
        {
            rotation.y = Random.Range(randomRange.min, randomRange.max);
        } else if (axis == Axis.z)
        {
            rotation.z = Random.Range(randomRange.min, randomRange.max);
        }
        else if (axis == Axis.All)
        {
            rotation.x = Random.Range(randomRange.min, randomRange.max);
            rotation.y = Random.Range(randomRange.min, randomRange.max);
            rotation.z = Random.Range(randomRange.min, randomRange.max);
        };


        if (rotationType == RotationType.LocalRotation)
        {
            trans.localEulerAngles = rotation;
        }
        else if (rotationType == RotationType.Rotation)
        {
            trans.eulerAngles = rotation;
        };
    }


}
