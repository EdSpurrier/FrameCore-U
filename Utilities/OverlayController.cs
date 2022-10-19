using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{

    [Title("Overlay Texture Settings")]
    public int minValue = 0;
    public int maxValue = 3;
    public string propertyName;
    public List<Renderer> overlayRenderers;



    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        propertyNameProp = Shader.PropertyToID(propertyName);
    }




    private MaterialPropertyBlock propertyBlock;
    private int propertyNameProp;





    public void SelectRandomOverlay(int overlayId)
    {
        overlayRenderers[overlayId].GetPropertyBlock(propertyBlock);
        propertyBlock.SetInt(propertyNameProp, Random.Range(minValue, maxValue));
        overlayRenderers[overlayId].SetPropertyBlock(propertyBlock);
    }
}
