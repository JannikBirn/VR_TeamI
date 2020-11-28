using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int hits;
    private float gameSpeed;
    private int lifes;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        gameSpeed = 1;
        lifes = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function resets all level elements within a game session
    // This is being used when a player looses a ball, but has not lost all of them yet
    public void reloadLevel() {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Trigger this when the player enters a trigger area in the middle of the disco sphere
    public void lostBall() {
        lifes--;
        if(lifes < 0) {
            gameIsLost();
        }
    }

    // This is being triggered if a player has lost his final life
    // This reloads the whole scene and the game begins again
    public void gameIsLost() {
        Debug.Log("You have lost");
        reloadLevel();
    }

    public void toggleMusic() {

    }

    // Getters & Setters

    public float getGameSpeed() {
        return gameSpeed;
    }

    public void setGameSpeed(float gameSpeed) {
        this.gameSpeed = gameSpeed;
    }

    // This method is connected to the OnDestroyed event of a BrickController
    // generated in the game. When called, it will schedule all of the provided
    // effects of that brick to be played
    public void OnBrickDestroyed(BrickEffect[] effects)
    {
        foreach (BrickEffect effect in effects)
        {
            StartCoroutine(effect.Apply());
        }
    }
}
