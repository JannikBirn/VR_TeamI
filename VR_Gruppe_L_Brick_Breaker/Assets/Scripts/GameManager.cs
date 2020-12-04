using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int hits;
    private float gameSpeed;
    private int lifes;
    private bool menu;
    public PlatformController plattform;
    public GameObject leaderboards;
    private bool showLeaderboards = false;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        gameSpeed = 1;
        lifes = 3;
        menu = false;
    }

    // Update is called once per frame
    void Update()
    {   
        // Plattform shrinks in size after some hits
        if(hits > 3) {
            plattform.setScale(0.75F, 0.75F);
        }
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

    public void toggleMenu() {

    }

    public void quitGame() {
        // Quit Game and open main menu scene
    }

    public void toggleLeaderboards() {
        leaderboards.SetActive(!leaderboards.activeSelf);
    }
    

    // Getters & Setters
    public int getHits() {
        return hits;
    }

    // Call this when the player hit another brick
    public void hit() {
        hits ++;
    }

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
