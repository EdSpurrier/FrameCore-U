using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class LightCurve : MonoBehaviour
{
    [HorizontalGroup("Info")]
    [HideLabel]
    public string lightCurveName = "";
    [HorizontalGroup("Info")]
    [HideLabel]
    public LightState lightState = LightState.On;

    [Title("Blend To State")]
    public SwitchState blendToLightState = SwitchState.Off;
    

    [TitleGroup("Shader Blend (Min >> Max)")]
    
    [HorizontalGroup("Shader Blend (Min >> Max)/Shader Blend Row")]
    [HideLabel]
    [HideIf("@this.blendToLightState == SwitchState.On && this.loopMode == AnimationLoopMode.Once")]
    public Color32 minColor;

    [HorizontalGroup("Shader Blend (Min >> Max)/Shader Blend Row")]
    [HideLabel]
    [HideIf("@this.blendToLightState == SwitchState.On && this.loopMode == AnimationLoopMode.Once")]
    [SuffixLabel("Min Intensity", Overlay = true)]
    public float minIntensity = 0f;

    [HorizontalGroup("Shader Blend (Min >> Max)/Shader Blend Row")]
    [HideLabel]
    public Color32 maxColor;
    [HorizontalGroup("Shader Blend (Min >> Max)/Shader Blend Row")]
    [HideLabel]
    [SuffixLabel("Max Intensity", Overlay = true)]
    public float maxIntensity = 2.5f;

    [TitleGroup("Light Blend (Min >> Max)")]
    [HorizontalGroup("Light Blend (Min >> Max)/Light Blend Row")]
    [HideLabel]
    [HideIf("@this.blendToLightState == SwitchState.On && this.loopMode == AnimationLoopMode.Once")]
    public Color32 minLightColor;

    [HorizontalGroup("Light Blend (Min >> Max)/Light Blend Row")]
    [HideLabel]
    [HideIf("@this.blendToLightState == SwitchState.On && this.loopMode == AnimationLoopMode.Once")]
    [SuffixLabel("Min Intensity", Overlay = true)]
    public float minLightIntensity = 0f;

    [HorizontalGroup("Light Blend (Min >> Max)/Light Blend Row")]
    [HideLabel]
    public Color32 maxLightColor;
    [HorizontalGroup("Light Blend (Min >> Max)/Light Blend Row")]
    [HideLabel]
    [SuffixLabel("Max Intensity", Overlay = true)]
    public float maxLightIntensity = 2.5f;



    [TitleGroup("Animation Curves")]
    [HorizontalGroup("Animation Curves/Animation Curves Row", 0.5f)]
    [HideLabel]
    public AnimationCurve angleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [HorizontalGroup("Animation Curves/Animation Curves Row", 0.5f)]
    [HideLabel]
    [Tooltip("Higher Number The Faster The Transition")]
    [SuffixLabel("Time Multiplier", Overlay = true)]
    public float GraphTimeMultiplier = 1;


    [Title("Settings")]
    public AnimationLoopMode loopMode = AnimationLoopMode.Once;
    public bool startAtEndOfState = false;

    [Title("Parts")]
    public List<Light> lights;
    public List<ShaderController> shaderControllers;

    [FoldoutGroup("System")]
    public Direction direction = Direction.Forward;
    [FoldoutGroup("System")]
    public float time = 0f;
    [FoldoutGroup("System")]
    public float blend = 0f;
    [FoldoutGroup("System")]
    public float normalizedIntensity = 0f;

    private bool canUpdate;
    private float startTime;


    [PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [Button("Refresh / Setup", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f)]
    private void Refresh()
    {
        if (lightState != LightState.Custom)
        {
            lightCurveName = lightState.ToString();
        };
    }


    public void CloneFrom(LightCurve lightCureToClone, bool includeLightsAndShaders = false)
    {
        lightCurveName = lightCureToClone.lightCurveName;
        //  Animation Curves
        angleCurve = lightCureToClone.angleCurve;
        GraphTimeMultiplier = lightCureToClone.GraphTimeMultiplier;
        //  Settings
        loopMode = lightCureToClone.loopMode;
        startAtEndOfState = lightCureToClone.startAtEndOfState;
        //  System
        direction = lightCureToClone.direction;
        time = lightCureToClone.time;
        blend = lightCureToClone.blend;
        normalizedIntensity = lightCureToClone.normalizedIntensity;

        if (includeLightsAndShaders)
        {
            lights.Clear();

            lightCureToClone.lights.ForEach(x => {
                lights.Add(x);
            });

            shaderControllers.Clear();

            lightCureToClone.shaderControllers.ForEach(x => {
                shaderControllers.Add(x);
            });

            shaderControllers.ForEach(x => x.Setup());
        };
        
    }

    private void Awake()
    {
        shaderControllers.ForEach( x => x.Setup() );
    }


    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
        direction = Direction.Forward;

        if (lights.Count == 0)
        {
            Debug.Log("Error >> No light sources connected....");
            //Debug.Break();
            return;
        };

        SetBlendStates();


        if (startAtEndOfState)
        {
            blend = angleCurve.keys[angleCurve.keys.Length - 1].value;
            UpdateLightBlend();

            canUpdate = false;

            /*float intensity = angleCurve.keys[angleCurve.keys.Length-1].value;
            lights.ForEach(l => l.intensity = intensity);

            normalizedIntensity = Mathf.Clamp(intensity, 0f, 1f);
            shaderControllers.ForEach(x => x.UpdateShader_Emission(normalizedIntensity));

            canUpdate = false;*/
            //Debug.Log("Clamped To End State");
        }
        else {
            blend = 0f;
            UpdateLightBlend();


            /*
                        float intensity = angleCurve.Evaluate(0);
                        lights.ForEach(l => l.intensity = intensity);

                        normalizedIntensity = Mathf.Clamp(intensity, 0f, 1f);
                        shaderControllers.ForEach(x => x.UpdateShader_Emission(normalizedIntensity));
            */

        };
        


        startAtEndOfState = false;
    }

    private void SetBlendStates()
    {
        if (blendToLightState == SwitchState.On)
        {
            shaderControllers.ForEach(shaderController =>
            {

                minColor = shaderController.currentEmission;
                minIntensity = shaderController.emissionOutput;

                shaderController.minColor = minColor;
                shaderController.minIntensity = minIntensity;

                shaderController.maxColor = maxColor;
                shaderController.maxIntensity = maxIntensity;
            });

            minLightIntensity = lights[0].intensity;
            minLightColor = lights[0].color;

        }
        else {
            shaderControllers.ForEach(shaderController => {

                shaderController.minColor = minColor;
                shaderController.minIntensity = minIntensity;

                shaderController.maxColor = maxColor;
                shaderController.maxIntensity = maxIntensity;
            });
        };

        
    }


    private void UpdateLightBlend()
    {
        blend = Mathf.Clamp(blend, 0f, 1f);

        lights.ForEach(l => {
            l.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, blend);
            l.color = Color.Lerp(minLightColor, maxLightColor, blend);
        });

        shaderControllers.ForEach(x => x.UpdateShader_Emission(blend));
    }

    private void Update()
    {
        if (direction == Direction.Forward)
        {
            time = Time.time - startTime;
        }
        else if (direction == Direction.Backward)
        {
            time = GraphTimeMultiplier - (Time.time - startTime);
        };



        if (canUpdate)
        {
            blend = angleCurve.Evaluate(time / GraphTimeMultiplier);


            
            



            UpdateLightBlend();

            /*intensity *= GraphIntensityMultiplier;

            lights.ForEach(l => l.intensity = intensity);*/
        };


        if (time >= GraphTimeMultiplier)
        {
            //  IF ONCE MODE THEN STOP AND DISABLE THIS
            if (loopMode == AnimationLoopMode.Once)
            {
                canUpdate = false;
                this.enabled = false;
                return;
            }
            else if (loopMode == AnimationLoopMode.Loop)
            {
                startTime = Time.time;
            }
            else if (loopMode == AnimationLoopMode.PingPong)
            {
                startTime = Time.time;

                direction = Direction.Backward;
            };




        }
        else if (time <= 0f && direction == Direction.Backward)
        {
            startTime = Time.time;
            direction = Direction.Forward;
        };
    }














}