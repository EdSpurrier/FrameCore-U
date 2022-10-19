using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Action_MovePosition
{

    public Transform referencePosition;
    public bool rotate = false;

    public List<Transform> transforms;

    [Title("System")]
    public DeBugger debug;
    


    public void Activate()
    {
        foreach(Transform trans in transforms)
        {
            trans.position = referencePosition.position;
            if (rotate)
            {
                trans.rotation = referencePosition.rotation;
            };
        };
    }

}
