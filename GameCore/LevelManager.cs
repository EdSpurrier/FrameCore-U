using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum LevelPlayState
{
    Unplayed,
    Incomplete,
    Failed,
    Win,
    Complete,
}

[System.Serializable]
public class LevelData
{
    public int levelId = 0;
    public string levelName = "Level #";
    public bool instructions = false;
    public bool countdown = false;
    public int highScore = 0;
    public LevelPlayState playState = LevelPlayState.Unplayed;
    public float lastPlaytime = 0f;
    public float longestPlaytime = 0f;
    public float shortestPlaytime = 0f;
    public string lastPlayDate = "";
    public int totalPlayCount = 0;

}


public class LevelManager : MonoBehaviour
{
    [BoxGroup("Data")]
    [HideLabel]
    public LevelData data;


    PlayerPrefData levelHighScore;

    public void Init()
    {

    }

    public void Start()
    {
        UpdateData();
    }


    void UpdateData()
    {
        levelHighScore = Frame.core.data.playerPrefManager.GetDataPoint(data.levelName);
        data.highScore = levelHighScore.GetIntValue();
    }

    public void SaveLevelHighScore()
    {


        if (data.highScore < Game.core.score.GetScore(ScoreType.Level))
        {
            levelHighScore.Save(Game.core.score.GetScore(ScoreType.Level));
        }
    }


}
