using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    public SoundController soundController;

    public void PlaySound(string soundName)
    {
        soundController.Play(soundName);
    }
}
