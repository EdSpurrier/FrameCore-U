using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameAction
{
    Start,
    Stop,
    Pause,
    Unpause,
    Win,
    Loose,
    Event
}


[System.Serializable]
public class Action_Game
{
    [InlineButton("TestAction")]
    [HorizontalGroup("Row1")]
    [EnumPaging]
    [EnumToggleButtons]
    [HideLabel]
    public GameAction gameAction;
    [ShowIf("gameAction", GameAction.Event)]
    [HorizontalGroup("Row1")]
    [HideLabel]
    [SuffixLabel("Event Name", overlay: true)]
    public string eventName;

    private void TestAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {


        if (gameAction == GameAction.Start)
        {
            Game.core.StartGame();

        }
        else if (gameAction == GameAction.Stop)
        {
            Game.core.StopGame();

        }
        else if(gameAction == GameAction.Win)
        {
            Game.core.GameWon();

        }
        else if (gameAction == GameAction.Loose)
        {
            Game.core.GameLost();

        }
        else if (gameAction == GameAction.Pause)
        {
            Game.core.Pause();

        }
        else if (gameAction == GameAction.Unpause)
        {
            Game.core.Unpause();

        }
        else if (gameAction == GameAction.Event)
        {
            Game.core.TriggerGameEvent(eventName);

        };

    }

}
