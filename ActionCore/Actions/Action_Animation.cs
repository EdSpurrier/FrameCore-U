using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_Animation
{


    [HorizontalGroup("Split", 0.35f)]
    [HideLabel]
    public Animator animator;

    [InlineButton("TriggerAnimation")]


    [EnumPaging]
    [EnumToggleButtons]
    [HorizontalGroup("Split", 0.65f)]
    [HideLabel]
    public AnimationTriggerType animationTriggerType;


    [HorizontalGroup("Parameters", 0.60f)]
    [HideLabel]
    [SuffixLabel("Parameter", Overlay = true)]
    public string parameterName;

    public enum AnimationTriggerType
    {
        Bool,
        Trigger,
        Int,
        Float
    }



    [ShowIf("animationTriggerType", AnimationTriggerType.Bool)]
    [HorizontalGroup("Parameters", 0.40f)]
    [HideLabel]
    public bool boolValue = true;

    [ShowIf("animationTriggerType", AnimationTriggerType.Int)]
    [HorizontalGroup("Parameters", 0.40f)]
    [HideLabel]
    public int intValue = 0;

    [ShowIf("animationTriggerType", AnimationTriggerType.Float)]
    [HorizontalGroup("Parameters", 0.40f)]
    [HideLabel]
    public float floatValue = 0f;

    [HideLabel]
    public DeBugger debug;

    private void TriggerAnimation()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {

        if (animationTriggerType == AnimationTriggerType.Bool)
        {
            animator.SetBool(parameterName, boolValue);
        }
        else if (animationTriggerType == AnimationTriggerType.Int)
        {
            animator.SetInteger(parameterName, intValue);
        }
        else if (animationTriggerType == AnimationTriggerType.Float)
        {
            animator.SetFloat(parameterName, floatValue);
        }
        else if (animationTriggerType == AnimationTriggerType.Trigger)
        {
            animator.SetTrigger(parameterName);
        };

        debug.Log("Animation Parameter Triggered >> " + animationTriggerType);
    }
}
