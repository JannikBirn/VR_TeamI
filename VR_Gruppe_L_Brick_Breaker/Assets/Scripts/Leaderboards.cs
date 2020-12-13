using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboards : MonoBehaviour
{
    private Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText  = GameObject.Find("Body").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setText(int score) {
        // Load actual Highscores here
        myText.text = "The Highscore: " + score;
    }

}
