using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Frame
{
    public static FrameCore core;
}


public class FrameCore : MonoBehaviour
{

    [Title("Cores")]
    [InlineButton("ConnectCores")]
    public EventCore events;
    public ActionCore actions;
    public DataCore data;
    public PoolCore pools;
    public SoundCore sound;
    public SceneCore scenes;
    public PlayerCore player;
    public InputCore input;

    [Space(10)]

    [Title("Scene Settings")]
    [HideLabel]
    public SceneSettings sceneSettings;

    [Space(10)]

    [Title("Layers")]
    [HideLabel]
    public LayerMasks layerMasks;

    [Space(10)]

    [Title("Highlighting")]
    [HideLabel]
    public Highlight highlights;

    [Space(10)]

    [Title("Statistics")]
    [HideLabel]
    public Statistics statistics;

    [Title("Frame Update")]

    [HideLabel]
    public Tools tools;


    bool systemError = false;




    private void ConnectCores()
    {
        if (EditorInteractions.InEditorButton())
        {
            events = CheckArrayForComponent( FindObjectsOfType(typeof(EventCore)) as EventCore[] ) as EventCore;
            actions = CheckArrayForComponent(FindObjectsOfType(typeof(ActionCore)) as ActionCore[]) as ActionCore;
            data = CheckArrayForComponent(FindObjectsOfType(typeof(DataCore)) as DataCore[]) as DataCore;
            pools = CheckArrayForComponent(FindObjectsOfType(typeof(PoolCore)) as PoolCore[]) as PoolCore;
            sound = CheckArrayForComponent(FindObjectsOfType(typeof(SoundCore)) as SoundCore[]) as SoundCore;
            scenes = CheckArrayForComponent(FindObjectsOfType(typeof(SceneCore)) as SceneCore[]) as SceneCore;
            input = CheckArrayForComponent(FindObjectsOfType(typeof(InputCore)) as InputCore[]) as InputCore;
            player = CheckArrayForComponent(FindObjectsOfType(typeof(PlayerCore)) as PlayerCore[]) as PlayerCore;

            EditorInteractions.SetDirty(this);
        };
    }

    Component CheckArrayForComponent(Component[] components)
    {
        if (components.Length > 1 || components.Length == 0)
        {
            Debug.LogError("FrameCore [ERROR] >> " + components.Length + " - " + components.GetType() + " Found!");
            return null;
        }
        else
        {
            Debug.Log("FrameCore >> Connected " + components.GetType());
            return components[0];
        };
    }



    void CheckComponent(Component component)
    {
        if (!component)
        {
            Debug.LogError("FrameCore [ERROR] >> Core Component Is Not Attached!");
            systemError = true;
        };
    }


    void CheckSetup()
    {
        CheckComponent(events);
        CheckComponent(actions);
        CheckComponent(data);
        CheckComponent(pools);
        CheckComponent(sound);
        CheckComponent(scenes);
        CheckComponent(input);
        CheckComponent(player);
        CheckComponent(sceneSettings); 

        if (systemError)
        {
            Debug.LogError("FrameCore Incorrectly Setup....");
            Debug.Break();
        };
    }


    //  CONNECT RUNTIME SCENE CORES
    void ConnectRuntimeCores()
    {
        sceneSettings = CheckArrayForComponent(FindObjectsOfType(typeof(SceneSettings)) as SceneSettings[]) as SceneSettings;
    }


    private void Awake()
    {
        Frame.core = this;
        
        ConnectRuntimeCores();

        CheckSetup();     

        Debug.Log("FrameCore Started...");


        tools.Init();
        data.Init();

        sceneSettings.Init();
    }




    private void Update()
    {
        statistics.Update();
        tools.Update();
    }

    private void FixedUpdate()
    {
        statistics.FixedUpdate();
        tools.FixedUpdate();
    }


    private void LateUpdate()
    {
        statistics.LateUpdate();
        tools.LateUpdate();
    }






    public void QuitApplication()
    {
        Application.Quit();
    }


}
