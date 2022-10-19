using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum SceneUpdateType
{   
    LoadScene,
    ActivateScene,
    UnloadScene
}



[System.Serializable]
public class Action_SceneUpdate
{

    [InlineButton("SceneUpdateAction")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public SceneUpdateType sceneUpdateType;

    
    [ShowIf("sceneUpdateType", SceneUpdateType.LoadScene)]
    [HideLabel] 
    public SceneCoreScene sceneCoreScene;
    [ShowIf("@this.sceneUpdateType == SceneUpdateType.ActivateScene || this.sceneUpdateType == SceneUpdateType.UnloadScene")]
    public string sceneName = "";

    private void SceneUpdateAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {


        if (sceneUpdateType == SceneUpdateType.ActivateScene)
        {
            Frame.core.scenes.ActivateScene(sceneName);
        }
        else if (sceneUpdateType == SceneUpdateType.LoadScene)
        {
            Frame.core.scenes.LoadScene(sceneCoreScene);
        }
        else if (sceneUpdateType == SceneUpdateType.UnloadScene)
        {
            Frame.core.scenes.UnloadScene(sceneName);
        };

    }


}
