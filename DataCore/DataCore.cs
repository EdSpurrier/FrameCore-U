using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCore : MonoBehaviour
{
    [FoldoutGroup("PlayerPref Data")]
    [HideLabel]
    public PlayerPrefManager playerPrefManager;


    public void Init()
    {
        playerPrefManager.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
