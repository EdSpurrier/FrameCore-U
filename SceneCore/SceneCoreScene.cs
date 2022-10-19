using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[System.Serializable]
public class SceneCoreScene
{
    public string sceneName;

    [Title("Load Settings")]
    public LoadSceneMode loadSceneMode;
    public ThreadPriority asyncSpeed;

    [Title("Activation Settings")]
    public bool activated = false;
    public bool activateOnLoad = true;




    [Title("System")]
    public float loadAmount = 0f;
    public float loadingTime = 0f;
    public bool loading = false;
    public DeBugger debug;


    AsyncOperation asyncLoad;

    public void ActivateScene()
    {
        if (activated)
        {
            return;
        };

        if (loading)
        {
            debug.Log("Cannot Activate - Loading Scene...");
            activateOnLoad = true;
            return;
        };


        debug.Log("Activating Scene");
        asyncLoad.allowSceneActivation = true;


        activated = true;
        Frame.core.scenes.SceneCompleted(this);
    }


    public void SetupLoadScene()
    {
        // Load data very slowly and try not to affect performance of the game.
        // Good for loading in the background while the game is playing.

        Application.backgroundLoadingPriority = asyncSpeed;

        loadingTime = 0f;
        loading = true;
        loadAmount = 0f;

    }


    public IEnumerator LoadAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        SetupLoadScene();

        asyncLoad = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

        asyncLoad.allowSceneActivation = false;


        while (!asyncLoad.isDone)
        {
            //Output the current progress
            loadAmount = asyncLoad.progress;

            // Check if the load has finished
            if (asyncLoad.progress >= 0.9f)
            {
                loading = false;

                if (activateOnLoad)
                {
                    ActivateScene();
                };

            };

            yield return null;
        }


        Debug.Log("Scene Loaded - Load Time: " + loadingTime);
    }





    // Update is called once per frame
    public void Update()
    {
        if (loading)
        {
            loadingTime += Time.deltaTime;
        };
    }




}
