using System.Collections;
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
        timer += Time.deltaTime;
    }

    //TODO needs to be called when level starts
    private void OnLevelStarted()
    {
        //TODO start timer, reset score
        currentScore = 0;
    }

    //Gets called by the BlockGeneratorScript Event when a block is destroyed
    private void OnBlockDestroyed(BrickEffect[] effects)
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

    private void OnDisable()
    {
        //Saving the highscore
        SaveLoadManager.SaveObject(scoreSavestate);
    }
}
