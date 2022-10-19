using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public static class Game
{
    public static GameCore core;
}


public class GameCore : MonoBehaviour
{
    [BoxGroup("Game State")]
    public bool gameActive = true;

    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [HideLabel]
    [FoldoutGroup("Level Manager")]
    public LevelManager level;

    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [HideLabel]
    [FoldoutGroup("Score Manager")]
    public ScoreManager score;


    [Title("InGame UI")]
    public Animator animator_InGameUI;


    [BoxGroup("Game Event Library")]
    public EventLibrary gameEventLibrary;


    [BoxGroup("Game Won")]
    public bool gameWon = false;
    [BoxGroup("Game Won")]
    public FrameCoreEvent gameWonEvent = new FrameCoreEvent
    {
        eventName = "Game Won"
    };

    [BoxGroup("Game Lost")]
    public bool gameLost = false;
    [BoxGroup("Game Lost")]
    public FrameCoreEvent gameLostEvent = new FrameCoreEvent
    {
        eventName = "Game Lost"
    };



    [BoxGroup("Game States")]
    [FoldoutGroup("Game States/Start & Start")]
    [HideLabel]
    public OnOffToggleEvent gameStartStopEvent = new OnOffToggleEvent
    {
        eventName = "Start & Start States",
        onEvent = new FrameCoreEvent
        {
            eventName = "Start"
        },
        offEvent = new FrameCoreEvent
        {
            eventName = "Start"
        }
    };



    [FoldoutGroup("Game States/Pause & Unpause")]
    [HideLabel]
    public OnOffToggleEvent gamePauseUnpauseEvent = new OnOffToggleEvent
    {
        eventName = "Pause & Unpause States",
        onEvent = new FrameCoreEvent
        {
            eventName = "Pause"
        },
        offEvent = new FrameCoreEvent
        {
            eventName = "Unpause"
        }
    };
    private void Awake()
    {
        Game.core = this;

        GetGameData();

        Connect();

    }

    private void Connect()
    {
        level = FindObjectOfType(typeof(LevelManager)) as LevelManager;
        if(!level)
        {
            Debug.LogError("GameCore [ERROR] >> LevelManager - Not Found In Scene!");
        };

        level.Init();


        score = FindObjectOfType(typeof(ScoreManager)) as ScoreManager;
        if (!score)
        {
            Debug.LogError("GameCore [ERROR] >> ScoreManager - Not Found In Scene!");
        };

        score.Init();


    }

    public void StartGame()
    {
        gameActive = true;
        gameStartStopEvent.ToggleEvent(true);
    }


    public void StopGame()
    {
        gameActive = false;
        gameStartStopEvent.ToggleEvent(false);
    }



    public void Pause()
    {
        Time.timeScale = 0f;
        gamePauseUnpauseEvent.ToggleEvent(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        gamePauseUnpauseEvent.ToggleEvent(false);
    }


    public void GameWon()
    {
        gameActive = false;
        level.SaveLevelHighScore();
        gameWonEvent.Activate();
        gameWon = true;
    }

    public void GameLost()
    {
        gameActive = false;
        gameLostEvent.Activate();
        gameLost = true;
    }


    public void GetGameData()
    {
        animator_InGameUI.SetBool("Instructions", level.data.instructions);
        animator_InGameUI.SetBool("Count Down", level.data.countdown);
        animator_InGameUI.SetInteger("Level Id", level.data.levelId);
        animator_InGameUI.SetBool("Game Data Received", true);
    }


    public void TriggerGameEvent(string eventName)
    {
        gameEventLibrary.TriggerEvent(eventName);
    }
}
