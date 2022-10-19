using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Action_Move
{

    public enum MoveType
    {
        Preset,
        ReferenceTransform,
    }


    

    public List<Transform> transformsToMove;

    [FoldoutGroup("Move Position")]
    [HideLabel]
    [HorizontalGroup("Move Position/Split")]
    public PositionType positionType = PositionType.World;

    [HorizontalGroup("Move Position/Split")]
    [HideLabel]
    public MoveType moveType = MoveType.ReferenceTransform;

    [FoldoutGroup("Move Position")]
    public Transform parent;

    [FoldoutGroup("Move Position")]
    [ShowIf("moveType", MoveType.Preset)]
    public Vector3 position;

    [FoldoutGroup("Move Position")]
    [ShowIf("moveType", MoveType.ReferenceTransform)]
    public Transform referenceTransform;


    [Title("System")]
    public DeBugger debug;
    


    public void Activate()
    {
        MoveTransformsToPosition();
    }

    public void MoveTransformsToPosition()
    {

        if (moveType == MoveType.ReferenceTransform)
        {
            position = referenceTransform.position;
        };

        foreach (Transform trans in transformsToMove)
        {
            if (parent)
            {
                trans.parent = parent;
            };
            
            if (positionType == PositionType.World)
            {
                trans.position = position;
            }
            else if (positionType == PositionType.Local)
            {
                trans.localPosition = position;
            };
        };
        
    }

    
}
