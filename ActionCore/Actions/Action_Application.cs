using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApplicationAction
{
    Quit,
}


[System.Serializable]
public class Action_Application
{
    [InlineButton("TestAction")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ApplicationAction applicationAction;



    private void TestAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {
        

        if (applicationAction == ApplicationAction.Quit)
        {
            Frame.core.QuitApplication();
        };

    }
}
