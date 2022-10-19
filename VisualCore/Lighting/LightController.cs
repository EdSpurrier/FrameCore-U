using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System;

public enum LightState
{
    On,
    Off,
    Malfunction,
    Die,
    FadeOnOff,
    Custom,
    Lock,
    Unlock,
    LowPower,
}

[RequireComponent(typeof(LightControllerFrameEditor))]
public class LightController : MonoBehaviour
{
    [SuffixLabel("Light Name", Overlay = true)]
    public string lightName = "";

    [Title("Settings")]
    public LightmapBakeType renderMode = LightmapBakeType.Realtime;
    public bool initiateOnStart = true;
    public bool startAtEndOfState = false;

    [Title("Parts")]
    public List<Light> lights;
    public List<ShaderController> shaderControllers;

    [TitleGroup("Light States")]
    [HorizontalGroup("Light States/Row1")]
    public LightState lightState = LightState.On;

    [HorizontalGroup("Light States/Row1")]
    [Button("Update State")]
    public void UpdateLightState()
    {
        if (EditorInteractions.InPlayerButton())
        {
            UpdateState();
        };
    }

    [HorizontalGroup("Light States/Row1")]
    [Button("New Light State")]
    public void NewLightState()
    {
        lightStates.RemoveAll(x => { return x == null; });

        if (EditorInteractions.InEditorButton())
        {
            LightCurve newLightCurve = gameObject.AddComponent<LightCurve>();

            newLightCurve.lightState = LightState.Custom;
            

            foreach (LightState lightStateType in Enum.GetValues(typeof(LightState)))
            {
                bool found = false;

                foreach (LightCurve lightCurve in lightStates)
                {
                    if (lightCurve.lightState == lightStateType)
                    {
                        found = true;
                        break;
                    };
                };

                if (!found)
                {
                    newLightCurve.lightState = lightStateType;
                    break;
                };
            }

            newLightCurve.lightCurveName = newLightCurve.lightState.ToString();
            
            newLightCurve.lights = lights.ToList();
            newLightCurve.shaderControllers = shaderControllers.ToList();

            lightStates.Add(newLightCurve);

        };
    }

    [TitleGroup("Light States")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    public List<LightCurve> lightStates;

    LightControllerFrameEditor lightControllerFrameEditor;

    [PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [Button("Refresh / Setup", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f)]
    private void Refresh()
    {
        if (!lightControllerFrameEditor) {
            lightControllerFrameEditor = GetComponent<LightControllerFrameEditor>();
            lightControllerFrameEditor.lightController = this;
        } else if (!lightControllerFrameEditor.lightController) {
            lightControllerFrameEditor.lightController = this;
        };

        ConnectLightsAndShaders();


        if (lightName == "")
        {
            if (transform.parent == null)
            {
                lightName = gameObject.name;
            }
            else
            {
                lightName = transform.parent.gameObject.name;
            };
        };

        UpdateRenderMode();

    }



    //  LIGHT BAKING >> RENDER MODES
    public void SetRenderMode(LightmapBakeType newRenderMode)
    {
        renderMode = newRenderMode;
        UpdateRenderMode();
    }

    public void UpdateRenderMode()
    {
        lights.ForEach(light => {

#if UNITY_EDITOR
            light.lightmapBakeType = renderMode;
#endif

        });
    }





    void ConnectLightsAndShaders()
    {
        lightStates.ForEach(x => {
            ConnectLightsAndShadersPerCurve(x);
        });
    }


    void ConnectLightsAndShadersPerCurve(LightCurve lightCurve)
    {
        //  CONNECT LIGHTS & SHADERS
        lightCurve.lights = lights;
        lightCurve.shaderControllers = shaderControllers;
    }


    //  UPDATE LIGHTSTATE LIGHT CURVE DATA
    //  IF DOES NOT EXIST THEN CREATE AND ADD IT
    public void UpdateLightStateData(LightCurve lightCurve)
    {
        LightCurve lightStateCurve = lightStates.First(x => x.lightState == lightCurve.lightState);

        if (!lightStateCurve) {
            lightStateCurve = new LightCurve();
            lightStates.Add(lightStateCurve);
        };

        lightStateCurve.CloneFrom(lightCurve);

        //  CONNECT LIGHTS & SHADERS
        ConnectLightsAndShadersPerCurve(lightStateCurve);
    }


    //  UPDATE LIGHTSTATE LIGHT CURVE DATA
    //  IF DOES NOT EXIST THEN CREATE AND ADD IT
    public void UpdateSpecificLightStateData(LightCurve lightCurve, LightState lightStateToUpdate)
    {
        LightCurve lightStateCurve = lightStates.First(x => x.lightState == lightStateToUpdate);

        if (!lightStateCurve)
        {
            lightStateCurve = new LightCurve();
            
            lightStates.Add(lightStateCurve);

        };

        lightStateCurve.CloneFrom(lightCurve);

        //  CONNECT LIGHTS & SHADERS
        ConnectLightsAndShadersPerCurve(lightStateCurve);
        lightStateCurve.lightState = lightStateToUpdate;
 
    }


    private void Start()
    {
        //  DISABLE LIGHT CONTROLLER IF LIGHT IS BAKED
        if (renderMode == LightmapBakeType.Baked)
        {
            this.enabled = false;
            return;
        };



        ResetLightStates();

        if (initiateOnStart)
        {

            if (startAtEndOfState)
            {
                lightStates.First(x => x.lightState == lightState).startAtEndOfState = true;
            };

            UpdateState();

        };

    }



    public void ResetLightStates()
    {
        lightStates.ForEach(x => {
            x.enabled = false;
        });
    }


    public void UpdateState()
    {
        ResetLightStates();

        lightStates.First(x => x.lightState == lightState).enabled = true;
    }

    public void SetState(LightState newState, bool resetLightState = false)
    {
        if (newState == lightState && !resetLightState)
        {
            return;
        };

        lightState = newState;


        UpdateState();
    }




    public void SetAtEndStateOfState(LightState newState)
    {
        lightState = newState;

        if (startAtEndOfState)
        {
            lightStates.First(x => x.lightState == lightState).startAtEndOfState = true;
        };

        UpdateState();
    }


    public void On()
    {
        SetState(LightState.On);
    }


    public void Off()
    {
        SetState(LightState.Off);
    }

    public void Malfunction()
    {
        SetState(LightState.Malfunction);
    }


    public void Die()
    {
        SetState(LightState.Die);
    }

    public void FadeOnOff()
    {
        SetState(LightState.FadeOnOff);
    }
}
