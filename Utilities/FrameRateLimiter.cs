using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FrameRateLimiter
{
    public int frameRate = 10;
    public int currentFrame = 0;

    public bool CheckFrame()
    {
        if (currentFrame >= frameRate)
        {
            currentFrame = 0;
            return true;
        };

        currentFrame++;
        return false;
    }
}
