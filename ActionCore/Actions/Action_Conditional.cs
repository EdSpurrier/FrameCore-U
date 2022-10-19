using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionalAction
{
    Score,
    ActiveState,
    Position,
    Rotation,
    Distance
}


public enum ConditionalType_Bool
{
    Equals,
    DoesNotEqual,
}


public enum ConditionalType_Value
{
    Equals,
    DoesNotEqual,
    LessThan,
    HigherThan,
}


[System.Serializable]
public class ScoreCondition
{
    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ScoreType scoreType;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ConditionalType_Value type;

    [HorizontalGroup("Row1")]
    [HideLabel]
    [SuffixLabel("Value", overlay: true)]
    public int value;

    [InlineButton("TriggerEventManager", "Trigger")]
    public EventManager resultEvent;


    public void Check()
    {
        if (CheckConditional())
        {
            resultEvent.Activate();
        };
    }

    public bool CheckConditional()
    {

        if (type == ConditionalType_Value.Equals)
        {
            return Equals();
        }
        else if (type == ConditionalType_Value.DoesNotEqual)
        {
            return DoesNotEqual();
        }
        else if (type == ConditionalType_Value.LessThan)
        {
            return LessThan();
        }
        else if (type == ConditionalType_Value.HigherThan)
        {
            return HigherThan();
        };

        return false;
    }



    public bool Equals()
    {
        return (Game.core.score.GetScore(scoreType) == value);
    }

    public bool DoesNotEqual()
    {
        return (Game.core.score.GetScore(scoreType) != value);
    }

    public bool LessThan()
    {
        return (Game.core.score.GetScore(scoreType) <= value);
    }

    public bool HigherThan()
    {
        return (Game.core.score.GetScore(scoreType) >= value);
    }

    private void TriggerEventManager()
    {
        if (EditorInteractions.InPlayerButton())
        {
            resultEvent.Activate();
        };
    }
}










[System.Serializable]
public class ActiveStateCondition
{
    [HorizontalGroup("Row1")]
    [HideLabel]
    public GameObject gameObject;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ConditionalType_Bool type;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ActiveState state;

    [InlineButton("TriggerEventManager", "Trigger")]
    public EventManager resultEvent;


    public void Check()
    {
        if (CheckConditional())
        {
            resultEvent.Activate();
        };
    }

    public bool CheckConditional()
    {

        if (type == ConditionalType_Bool.Equals)
        {
            return Equals();
        }
        else if (type == ConditionalType_Bool.DoesNotEqual)
        {
            return DoesNotEqual();
        };

        return false;
    }



    public bool Equals()
    {
        return (gameObject.activeSelf == (ActiveState.Active == state));
    }

    public bool DoesNotEqual()
    {
        return (gameObject.activeSelf != (ActiveState.Active == state));
    }

    private void TriggerEventManager()
    {
        if (EditorInteractions.InPlayerButton())
        {
            resultEvent.Activate();
        };
    }
}





[System.Serializable]
public class PositionCondition
{

    [HorizontalGroup("Row1")]
    [HideLabel]
    public Transform transform;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ConditionalType_Bool type;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public PositionPointType positionType;
    

    [HorizontalGroup("Row1")]
    [ShowIf("@this.positionType == PositionPointType.Transform")]
    [HideLabel]
    public Transform otherTransform;

    [ShowIf("@this.positionType == PositionPointType.Vector")]
    [HideLabel]
    public Vector3 vector;

    [InlineButton("TriggerEventManager", "Trigger")]
    public EventManager resultEvent;


    public void Check()
    {
        if (CheckConditional())
        {
            resultEvent.Activate();
        };
    }

    public bool CheckConditional()
    {

        if (type == ConditionalType_Bool.Equals)
        {
            return Equals();
        }
        else if (type == ConditionalType_Bool.DoesNotEqual)
        {
            return DoesNotEqual();
        };

        return false;
    }



    public bool Equals()
    {
        return (transform.position == (positionType == PositionPointType.Transform ? otherTransform.position : vector));
    }

    public bool DoesNotEqual()
    {
        return (transform.position != (positionType == PositionPointType.Transform ? otherTransform.position : vector));
    }

    private void TriggerEventManager()
    {
        if (EditorInteractions.InPlayerButton())
        {
            resultEvent.Activate();
        };
    }
}



[System.Serializable]
public class RotationCondition
{

    [HorizontalGroup("Row1")]
    [HideLabel]
    public Transform transform;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ConditionalType_Bool type;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public PositionPointType rotationType;


    [HorizontalGroup("Row1")]
    [ShowIf("@this.positionType == PositionPointType.Transform")]
    [HideLabel]
    public Transform otherTransform;

    [ShowIf("@this.positionType == PositionPointType.Vector")]
    [HideLabel]
    public Vector3 vector;

    [InlineButton("TriggerEventManager", "Trigger")]
    public EventManager resultEvent;


    public void Check()
    {
        if (CheckConditional())
        {
            resultEvent.Activate();
        };
    }

    public bool CheckConditional()
    {

        if (type == ConditionalType_Bool.Equals)
        {
            return Equals();
        }
        else if (type == ConditionalType_Bool.DoesNotEqual)
        {
            return DoesNotEqual();
        };

        return false;
    }



    public bool Equals()
    {
        return (transform.eulerAngles == (rotationType == PositionPointType.Transform ? otherTransform.eulerAngles : vector));
    }

    public bool DoesNotEqual()
    {
        return (transform.eulerAngles != (rotationType == PositionPointType.Transform ? otherTransform.eulerAngles : vector));
    }

    private void TriggerEventManager()
    {
        if (EditorInteractions.InPlayerButton())
        {
            resultEvent.Activate();
        };
    }
}










[System.Serializable]
public class DistanceCondition
{
    [HorizontalGroup("Row1")]
    [HideLabel]
    public Transform transform;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public PositionPointType positionType;


    [HorizontalGroup("Row1")]
    [ShowIf("@this.positionType == PositionPointType.Transform")]
    [HideLabel]
    public Transform otherTransform;

    [ShowIf("@this.positionType == PositionPointType.Vector")]
    [HideLabel]
    public Vector3 vector;



    [HorizontalGroup("Row2")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ConditionalType_Value type;

    [HorizontalGroup("Row2")]
    [HideLabel]
    [SuffixLabel("Value", overlay: true)]
    public int value;

    [InlineButton("TriggerEventManager", "Trigger")]
    public EventManager resultEvent;


    public void Check()
    {
        if (CheckConditional())
        {
            resultEvent.Activate();
        };
    }

    public bool CheckConditional()
    {

        if (type == ConditionalType_Value.Equals)
        {
            return Equals();
        }
        else if (type == ConditionalType_Value.DoesNotEqual)
        {
            return DoesNotEqual();
        }
        else if (type == ConditionalType_Value.LessThan)
        {
            return LessThan();
        }
        else if (type == ConditionalType_Value.HigherThan)
        {
            return HigherThan();
        };

        return false;
    }



    public float GetDistance()
    {
        return Vector3.Distance(transform.position, (positionType == PositionPointType.Transform ? otherTransform.position : vector));
    }


    public bool Equals()
    {
        return (GetDistance() == value);
    }

    public bool DoesNotEqual()
    {
        return (GetDistance() == value);
    }

    public bool LessThan()
    {
        return (GetDistance() <= value);
    }

    public bool HigherThan()
    {
        return (GetDistance() >= value);
    }

    private void TriggerEventManager()
    {
        if (EditorInteractions.InPlayerButton())
        {
            resultEvent.Activate();
        };
    }
}











/*public enum ConditionConditional
{
    AllConditions,
    OneCondition,
    NoConditions
}*/


[System.Serializable]
public class Action_Conditional
{
    [InlineButton("TestAction")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ConditionalAction conditionalAction;



    [ShowIf("@this.conditionalAction == ConditionalAction.Score")]
    public List<ScoreCondition> scoreConditions;


    [ShowIf("@this.conditionalAction == ConditionalAction.ActiveState")]
    public List<ActiveStateCondition> activeStateConditions;


    [ShowIf("@this.conditionalAction == ConditionalAction.Position")]
    public List<PositionCondition> positionConditions;

    [ShowIf("@this.conditionalAction == ConditionalAction.Rotation")]
    public List<RotationCondition> rotationConditions;

    [ShowIf("@this.conditionalAction == ConditionalAction.Distance")]
    public List<DistanceCondition> distanceConditions;


    /*public ConditionConditional conditionalEvent;

    [InlineButton("TriggerEventManager", "Trigger")]
    public EventManager resultEvent;*/

    private void TestAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {


        if (conditionalAction == ConditionalAction.Score)
        {
            scoreConditions.ForEach(condition => { condition.Check(); });
        }
        else if (conditionalAction == ConditionalAction.ActiveState)
        {
            activeStateConditions.ForEach(condition => { condition.Check(); });
        }
        else if (conditionalAction == ConditionalAction.Position)
        {
            positionConditions.ForEach(condition => { condition.Check(); });
        }
        else if (conditionalAction == ConditionalAction.Rotation)
        {
            rotationConditions.ForEach(condition => { condition.Check(); });
        }
        else if (conditionalAction == ConditionalAction.Distance)
        {
            distanceConditions.ForEach(condition => { condition.Check(); });
        };


    }


}
