using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Leaderboards : MonoBehaviour
{
    // Diese Klasse dient zum aktualisieren des TextMeshes, welches den aktuellen Highscore in korrekter Formatierung darstellen soll. Hierzu wird der Highscore vom ScoreManager angefragt
    public Text myText;
    public ScoreManager score;

    private void OnEnable()
    {
        setText();
    }

    public void setText()
    {
        //TODO: highscore does not refresh within a round
        // set Leaderboards text to current Highscore
        if (score.getHighscore().highScore >= score.getCurrentScore())
        {
            string date = new System.DateTime(score.getHighscore().timeStamp).ToString();
            myText.text = "The Highscore: " + score.getHighscore().highScore + " \n Seconds Survived : " + Mathf.Round(score.getHighscore().time) + " \n Date: " + date;
        }
        else
        {
            myText.text = "The Highscore: " + score.getCurrentScore();
        }
    }

}
