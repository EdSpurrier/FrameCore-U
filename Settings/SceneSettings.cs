using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    SpashScreen,
    Menu,
    Game,
    CutScene,
}




public class SceneSettings : MonoBehaviour
{
    [Title("Scene Settings")]
    public SceneType sceneType;
    public bool disableScreenSleep = true;



    [Title("Scene Parts")]
    public Animator sceneAnimatorUI;

    public void Init()
    {
        if (disableScreenSleep)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        };
        

        switch (sceneType)
        {
            case SceneType.SpashScreen:
                SetSpashScreenScene();
                break;
            case SceneType.Menu:
                SetMenuScene();
                break;
            case SceneType.Game:
                SetGameScene();
                break;
            case SceneType.CutScene:
                SetCutSceneScene();
                break;
            default:
                SetGameScene();
                break;
        };
    }


    void SetSpashScreenScene()
    {
        Frame.core.player.enabled = false;
    }
    void SetMenuScene()
    {
        Frame.core.player.enabled = false;
    }
    void SetGameScene()
    {

    }
    void SetCutSceneScene()
    {
        Frame.core.player.enabled = false;
    }

}
