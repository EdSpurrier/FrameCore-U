using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_SoundFX
{
    

    [HideLabel]
    public SoundController soundController;

    
    


    [HorizontalGroup("Row1", 0.17f)]
    [PropertyOrder(2)]
    [Button("<<")]
    public void Previous()
    {
        Select(-1);
    }

    [HorizontalGroup("Row1", 0.16f)]
    [HideLabel]
    [PropertyOrder(3)]
    public int soundId = 0;

    [HorizontalGroup("Row1", 0.8f)]
    [HideLabel]
    [PropertyOrder(4)]
    public string soundName = "No Sound Selected...";

    [HorizontalGroup("Row1", 0.17f)]
    [PropertyOrder(5)]
    [Button(">>")]
    public void Next()
    {
        Select(1);
    }

    void Select(int direction)
    {
        if (!soundController)
        {
            soundName = "No Sounds Found!";
            return;
        };

        int count = soundController.sounds.Count;

        if (count < 0)
        {
            return;
        };
        
        count--;

        soundId += direction;


        if (soundId > count)
        {
            soundId = 0;
        }
        else if (soundId < 0)
        {
            soundId = count;
        };


        soundName = soundController.sounds[soundId].soundName;

    }



    public void Activate()
    {
        soundController.Play(soundId);
    }
    

   
}
