using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WebAction
{
    OpenURL,
    OpenPresetURL
}


[System.Serializable]
public class Action_Web
{
    [InlineButton("TestAction")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public WebAction webAction;

    [ShowIf("@this.webAction == WebAction.OpenURL")]
    public string url = "http://www.google.com/";

    [ShowIf("@this.webAction == WebAction.OpenPresetURL")]
    public OpenURL openURL;
    [ShowIf("@this.webAction == WebAction.OpenPresetURL")]
    public string presetName;
    private void TestAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {


        if (webAction == WebAction.OpenURL)
        {
            Application.OpenURL(url);
        } else if (webAction == WebAction.OpenPresetURL)
        {
            openURL.OpenPresetURL(presetName);
        };

    }

}
