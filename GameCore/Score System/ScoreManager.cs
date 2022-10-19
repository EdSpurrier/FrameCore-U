using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public enum ScoreType
{
    Game,
    Level,
}



[System.Serializable]
public class Score
{
    public ScoreType type;
    public int score = 0;
    public bool gameWonOnMaxScore = true;
    public int maxScore = 100;
    public string playerPrefId;
    public bool negativeScore = false;

    [Title("Score Text UI")]
    public Text scoreUI;
    public string preText = "SCORE: ";
    public string postText = "!";
    public Image scoreImage;
    


    [BoxGroup("Score Events")]
    [FoldoutGroup("Score Events/Add Score")]
    [HideLabel]
    public FrameCoreEvent addScoreEvent = new FrameCoreEvent
    {
        eventName = "Add Score"
    };

    [FoldoutGroup("Score Events/Subtract Score")]
    [HideLabel]
    public FrameCoreEvent subtractScoreEvent = new FrameCoreEvent
    {
        eventName = "Subtract Score"
    };

    [FoldoutGroup("Score Events/Max Score Reached")]
    [HideLabel]
    public FrameCoreEvent maxScoreReachedEvent = new FrameCoreEvent
    {
        eventName = "Max Score Reached"
    };

    public void Init()
    {
        UpdateScoreUI();
    }

    public void UpdateScore(int value)
    {
        if(!Game.core.gameActive)
        {
            return;
        };

        score += value;

        if(score < 0 && !negativeScore)
        {
            score = 0;
        };

        if (value > 0f)
        {
            addScoreEvent.Activate();
        }
        else {
            subtractScoreEvent.Activate();
        };


        if (score > maxScore)
        {
            score = maxScore;
        };

        UpdateScoreUI();



        if(score >= maxScore)
        {
            maxScoreReachedEvent.Activate();

            if (gameWonOnMaxScore)
            {
                Game.core.GameWon();
            };
        };
    }

    public void UpdateScoreUI()
    {
        if (!scoreUI)
        {
            return;
        };

        if (scoreUI)
        {
            scoreUI.text = ScoreOutput();
        };

        if (scoreImage)
        {
            scoreImage.fillAmount = ((float)score / (float)maxScore);
        };
        
    }

    public string ScoreOutput()
    {
        return preText + score.ToString() + postText;
    }


}


public class ScoreManager : MonoBehaviour
{
    public List<Score> scores;

    public void Init()
    {
        scores.ForEach(score => { score.Init();  });
    }

    public void UpdateScore(int value, ScoreType scoreType) { 
    
        scores.First(score => (score.type == scoreType)).UpdateScore(value);
    }

    public void UpdateScoreUI(ScoreType scoreType)
    {

        scores.First(score => (score.type == scoreType)).UpdateScoreUI();
    }

    public int GetScore(ScoreType scoreType)
    {
        return scores.First(score => (score.type == scoreType)).score;
    }
}
