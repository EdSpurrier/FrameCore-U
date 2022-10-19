using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[System.Serializable]
public class FrameInterfaceButtonControl
{
    [FoldoutGroup("$buttonName")]

    [HorizontalGroup("$buttonName/Row_1")]
    [HideLabel]
    public KeyCode shortcut;
    
    [HorizontalGroup("$buttonName/Row_1")]
    [HideLabel]
    public string buttonName;

    [HorizontalGroup("$buttonName/Row_2")]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    [HideLabel]
    public FrameInterfaceButton button;
}

public class FrameInterfaceButtonManager : MonoBehaviour
{

    public List<FrameInterfaceButtonControl> buttons;
    

    
    [FoldoutGroup("System")]
    [HideLabel]
    public DeBugger debug;


    private void OnValidate()
    {
        buttons.ForEach(button => {
            button.buttonName = button.button.buttonName;
        });
    }

    // Update is called once per frame
    void Update()
    {
        buttons.ForEach(button => {
            if (Input.GetKeyDown(button.shortcut))
            {
                debug.Log(button.shortcut + " was pressed.");
                
                button.button.ButtonDown();

            } 
            else if (Input.GetKeyUp(button.shortcut))
            {
                debug.Log(button.shortcut + " was released.");

                button.button.ButtonUp();

            };
        });
    }
}
