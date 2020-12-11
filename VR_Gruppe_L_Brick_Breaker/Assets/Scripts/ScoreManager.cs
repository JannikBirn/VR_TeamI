﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public int scoreOfABlock;

    //Reference to the Score Savestate
    private ScoreSavestate scoreSavestate = new ScoreSavestate();

    private int currentScore; //score of the current level
    private float timer;


    private void Awake()
    {
        //Trying to load the scoreSavestate if one was already saved
        SaveLoadManager.LoadObject(scoreSavestate);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(scoreSavestate.highScore);
        Debug.Log(new DateTime(scoreSavestate.timeStamp).ToString());
    }

    private void Update()
    {
        if (LevelEvent.state == LevelEvent.STATE_PLAYING)
        {
            //Adding time to the timer if player is playing
            timer += Time.unscaledDeltaTime;
        }
    }

    public void onLevelEvent(int levelEvent)
    {
        if (levelEvent == LevelEvent.LEVEL_START)
        {
            currentScore = 0;
            timer = 0;
        }
        else if (levelEvent == LevelEvent.LEVEL_STOP)
        {
            OnLevelFinished();
        }
    }


    //Gets called by the BlockGeneratorScript Event when a block is destroyed
    public void OnBlockDestroyed(BrickEffect[] effects)
    {
        Debug.Log("ScoreManager : OnBlockDestroyed() Event call");
        currentScore += scoreOfABlock;
    }

    //TODO needs to be called when a level finished
    private void OnLevelFinished()
    {
        if (currentScore > scoreSavestate.highScore)
        {
            Debug.Log("ScoreManager : new HighScore");
            //Got a new highscore
            scoreSavestate.highScore = currentScore;
            //Getting the current Time
            scoreSavestate.timeStamp = System.DateTime.Now.Ticks;
            //TODO set time

            SaveLoadManager.SaveObject(scoreSavestate);
        }
    }

    public float getTime()
    {
        return timer;
    }

    private void OnDisable()
    {
        //Saving the highscore
        SaveLoadManager.SaveObject(scoreSavestate);
    }
}
