using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum ButtonTriggerType
{
    Single,
    Toggle,
    Hold
}

public enum ButtonGraphicType
{
    Text,
    Icon,
}


public enum ButtonState
{
    On,
    Off
}


public class FrameInterfaceButton : MonoBehaviour, IPointerUpHandler, IPointerClickHandler
{
    [BoxGroup("Button Events")]

    [FoldoutGroup("Button Events/On")]
    [HideLabel]
    public FrameCoreEvent onEvent;

    [FoldoutGroup("Button Events/Off")]
    [HideLabel]
    public FrameCoreEvent offEvent;

    [BoxGroup("Button Settings")]
    [EnumPaging]
    [HideLabel]
    [SuffixLabel("Name", Overlay = true)]
    public string buttonName;



    [BoxGroup("Button Settings")]
    [EnumPaging]
    [HideLabel]
    public ButtonTriggerType triggerType;

    [BoxGroup("Button Settings")]
    [EnumPaging]
    [HideLabel]
    public ButtonGraphicType graphicType;


    [BoxGroup("Button Settings")]
    [ShowIf("@this.graphicType == ButtonGraphicType.Icon")]
    [EnumPaging]
    [HideLabel]
    public Size iconSize;

    [BoxGroup("Button Settings")]
    [ShowIf("@this.graphicType == ButtonGraphicType.Text")]
    [EnumPaging]
    [HideLabel]
    public Size fontSize;


    [BoxGroup("Button Settings")]
    [ShowIf("@this.graphicType == ButtonGraphicType.Icon")]
    [AssetSelector(Paths = "Assets/Core/GFX/Icons")]
    [HideLabel]
    [SuffixLabel("Icon", Overlay = true)]
    public Texture icon;

    

    [BoxGroup("Button Settings")]
    [ShowIf("@this.graphicType == ButtonGraphicType.Icon && this.triggerType != ButtonTriggerType.Single")]
    [AssetSelector(Paths = "Assets/Core/GFX/Icons")]
    [HideLabel]
    [SuffixLabel("Icon", Overlay = true)]
    public Texture iconActive;

    [BoxGroup("Button Settings")]
    [ShowIf("@this.graphicType == ButtonGraphicType.Text")]
    [HideLabel]
    [SuffixLabel("Label", Overlay = true)]
    public string label;

    

    [BoxGroup("Button Colours")]

    [BoxGroup("Button Colours/Active")]
    public Color activeBackgroundColour;
    [BoxGroup("Button Colours/Active")]
    public Color activeForegroundColour;


    [BoxGroup("Button Colours/Inactive")]
    [ShowIf("@this.triggerType != ButtonTriggerType.Single")]
    public Color inactiveBackgroundColour;

    [BoxGroup("Button Colours/Inactive")]
    [ShowIf("@this.triggerType != ButtonTriggerType.Single")]
    public Color inactiveForegroundColour;


    [FoldoutGroup("Parts")]
    [HideLabel]
    public Text text;

    [FoldoutGroup("Parts")]
    [HideLabel]
    public RawImage iconImage;

    [FoldoutGroup("Parts")]
    [HideLabel]
    public RawImage backgroundImage;

    [FoldoutGroup("System")]
    [HideLabel]
    public ButtonState state = ButtonState.Off;


    

    [FoldoutGroup("System")]
    [HideLabel]
    [InlineButton("Build")]
    public DeBugger debug;



    private void OnValidate()
    {
        Build();
    }


    void Build()
    {
        iconImage.texture = icon;
        text.text = label;

        gameObject.name = buttonName + " Button";

        text.gameObject.SetActive(graphicType == ButtonGraphicType.Text);
        iconImage.gameObject.SetActive(graphicType == ButtonGraphicType.Icon);


        iconImage.rectTransform.sizeDelta = FrameInterface.core.GetIconSize(iconSize);
        text.fontSize = FrameInterface.core.GetFontSize(fontSize);

        if (triggerType == ButtonTriggerType.Single)
        {
            backgroundImage.color = activeBackgroundColour;
            iconImage.color = activeForegroundColour;
            text.color = activeForegroundColour;
        }
        else
        {
            backgroundImage.color = inactiveBackgroundColour;
            iconImage.color = inactiveForegroundColour;
            text.color = inactiveForegroundColour;
        }

        EditorInteractions.SetDirty(gameObject);
        EditorInteractions.SetDirty(iconImage.gameObject);
        EditorInteractions.SetDirty(text.gameObject);
    }


    public void ButtonDown()
    {
        if (triggerType == ButtonTriggerType.Toggle)
        {
            Toggle();
        }
        else if (triggerType == ButtonTriggerType.Single)
        {
            TriggerOn();
        }
        else if (triggerType == ButtonTriggerType.Hold)
        {
            TriggerOn();
        };
    }

    public void ButtonUp()
    {
        if (triggerType == ButtonTriggerType.Hold)
        {
            TriggerOff();
        };
    }


    public void Toggle()
    {
        if (state == ButtonState.On)
        {
            TriggerOff();
        }
        else if (state == ButtonState.Off)
        {
            TriggerOn();
        };
    }

    public void TriggerOn()
    {
        state = ButtonState.On;

        onEvent.Activate();

        debug.Log("Trigger On");

        backgroundImage.color = activeBackgroundColour;
        iconImage.color = activeForegroundColour;
        text.color = activeForegroundColour;

        if (triggerType != ButtonTriggerType.Single)
        {
            iconImage.texture = iconActive;
        }

    }

    public void TriggerOff()
    {
        state = ButtonState.Off;

        offEvent.Activate();

        debug.Log("Trigger Off");

        backgroundImage.color = inactiveBackgroundColour;
        iconImage.color = inactiveForegroundColour;
        text.color = inactiveForegroundColour;

        if (triggerType != ButtonTriggerType.Single)
        {
            iconImage.texture = icon;
        }
    }



    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");

        ButtonDown();
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("The mouse click was released");

        ButtonUp();
    }
}
