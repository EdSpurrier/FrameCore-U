using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneCore : MonoBehaviour
{

    public List<SceneCoreScene> scenes;


    SceneCoreScene GetSceneByName(string sceneName)
    {
        SceneCoreScene sceneFound = null;

        foreach (SceneCoreScene scene in scenes)
        {
            if (scene.sceneName == sceneName)
            {
                sceneFound = scene;
            };
        };

        return sceneFound;
    }


    public void UnloadScene(string sceneName)
    {
        SceneCoreScene sceneCoreScene = GetSceneByName(sceneName);
        
        if (sceneCoreScene != null)
        {
            scenes.Remove(sceneCoreScene);
        };

        SceneManager.UnloadSceneAsync(sceneName);
    }






    public void ActivateScene(string sceneName)
    {

        SceneCoreScene sceneCoreScene = GetSceneByName(sceneName);

        if (sceneCoreScene != null)
        {
            sceneCoreScene.activateOnLoad = true;
            sceneCoreScene.ActivateScene();
        }
        else
        {
            Debug.LogError("Cant Activate - Scene Not Loaded [SceneName:" + sceneCoreScene.sceneName + "]");
        };
    }




    public void LoadScene(SceneCoreScene sceneCoreScene)
    {

        if (GetSceneByName(sceneCoreScene.sceneName) == null)
        {
            scenes.Add(sceneCoreScene);
            StartCoroutine( sceneCoreScene.LoadAsyncScene() );
        }
        else
        {
            Debug.LogError("Scene Already Loaded [SceneName:" + sceneCoreScene.sceneName + "]");
        };
    }


    public void SceneCompleted(SceneCoreScene sceneCoreScene)
    {
        if (scenes.Contains(sceneCoreScene))
        {
            scenes.Remove(sceneCoreScene);
        };
    }


    public void Update()
    {
        foreach(SceneCoreScene scene in scenes)
        {
            scene.Update();
        };
    }

}
