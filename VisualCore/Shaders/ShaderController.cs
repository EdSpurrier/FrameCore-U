using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;

public class ShaderController : MonoBehaviour
{
    [Title("Settings")]
    [PropertyOrder(1)]
    public Renderer rend;
    
    [HideInInspector]
    public Material material;

    //  MATERIAL MANAGER
    bool rendererFound = false;
    List<Material> materialsFound = new List<Material>();
    

    [ShowIfGroup("rendererFound")]
    
    [TitleGroup("rendererFound/Material")]

    [HorizontalGroup("rendererFound/Material/Row1", 0.3f)]
    [PropertyOrder(2)]
    [Button("Previous")]
    public void PreviousMaterial()
    {
        SelectMaterial(-1);
    }

    [HorizontalGroup("rendererFound/Material/Row1", 0.15f)]
    [HideLabel]
    [PropertyOrder(3)]
    public int materialIndex = 1;

    [HorizontalGroup("rendererFound/Material/Row1", 0.25f)]
    [HideLabel]
    [PropertyOrder(4)]
    public string materialName = "";
    
    [HorizontalGroup("rendererFound/Material/Row1", 0.3f)]
    [PropertyOrder(5)]
    [Button("Next")]
    public void NextMaterial()
    {
        SelectMaterial(1);
    }

    void SelectMaterial(int direction)
    {
        materialsFound.Clear();

        foreach(Material mat in rend.sharedMaterials) {
            materialsFound.Add(mat);
        }

        int materialCount = materialsFound.Count;

        if (materialCount < 0)
        {
            return;
        };

        materialCount--;

        materialIndex += direction;

        if (materialIndex > materialCount)
        {
            materialIndex = 0;
        } else if (materialIndex < 0)
        {
            materialIndex = materialCount;
        };

        materialName = materialsFound[materialIndex].name;

        currentEmission = rend.sharedMaterials[materialIndex].GetColor("_EmissionColor");
    }



    


    [TitleGroup("Current Emission")]
    [PropertyOrder(6)]
    [HideLabel]
    public Color currentEmission;


    [TitleGroup("Emission Color Range")]
    [InlineButton("GetMin")]
    [PropertyOrder(7)]
    public Color32 minColor;
    [PropertyOrder(7)]
    public float minIntensity = 0f;

    [InlineButton("GetMax")]
    [PropertyOrder(8)]
    public Color32 maxColor;
    [PropertyOrder(8)]
    public float maxIntensity = 2.5f;

    void GetMin()
    {
        minColor = rend.sharedMaterials[materialIndex].GetColor("_EmissionColor");
    }

    void GetMax()
    {
        maxColor = rend.sharedMaterials[materialIndex].GetColor("_EmissionColor") / maxIntensity;
    }

/*
    [TitleGroup("Emission Range")]
    [PropertyOrder(9)]
    [PropertySpace(SpaceAfter = 20)]
    [HideLabel]
    public MaxMinFloat emissionRange = new MaxMinFloat
    {
        min = 0f,
        max = 1f
    };*/

  

    

    [FoldoutGroup("System")]
    [PropertyOrder(10)]
    public Color outputEmission;

    [FoldoutGroup("System")]
    [PropertyOrder(14)]
    public float emissionOutput;

    [PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [Button("Refresh / Setup", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f)]
    private void Refresh()
    {
        if (!rend)
        {
            rend = GetComponent<Renderer>();
            materialIndex = 0;
            material = rend.sharedMaterials[materialIndex];
        };

        rendererFound = rend;

        currentEmission = rend.sharedMaterials[materialIndex].GetColor("_EmissionColor");
    }


    bool setup = false;


    public void Setup()
    {

        material = rend.materials[materialIndex];
        material.EnableKeyword("_EMISSION");
        material.EnableKeyword("_Emission");

        currentEmission = material.GetColor("_EmissionColor");
        
        //setup = true;
    }


    public void UpdateShader_Emission(float emissionAmount)
    {
        /*if (!setup)
        {
            Setup();    
        };*/

        emissionOutput = Mathf.Lerp(minIntensity, maxIntensity, emissionAmount);
        currentEmission = Color.Lerp(minColor, maxColor, emissionAmount);

        outputEmission = currentEmission * emissionOutput;

        material.SetColor("_EmissionColor", outputEmission);

    }
}
