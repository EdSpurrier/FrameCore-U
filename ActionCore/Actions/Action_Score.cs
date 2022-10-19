using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreAction
{
    ScoreUpdate,
    UpdateScoreUI,
}


[System.Serializable]
public class Action_Score
{
    [InlineButton("TestAction")]
    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ScoreAction scoreAction;

    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public ScoreType scoreType;

    [ShowIf("@this.scoreAction == ScoreAction.ScoreUpdate")]
    [HorizontalGroup("Row1")]
    [SuffixLabel("Value", overlay:true)]
    [HideLabel]
    public int scoreValue = 1;

    private void TestAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {


        if (scoreAction == ScoreAction.ScoreUpdate)
        {
            Game.core.score.UpdateScore(scoreValue, scoreType);

        } else if (scoreAction == ScoreAction.UpdateScoreUI)
        {
            Game.core.score.UpdateScoreUI(scoreType);
        };

    }

}
