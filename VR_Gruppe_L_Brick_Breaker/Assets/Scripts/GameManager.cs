using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int hits;
    private float gameSpeed;
    private int lives;
    public PlatformController plattform;
    public GameObject leaderboards;
    public GameObject bottomMenu;
    public GameObject startMenu;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        gameSpeed = 1;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {   
        // Plattform shrinks in size after some hits
        if(hits > 3) {
            plattform.setScale(0.75F, 0.75F);
        }

        Time.timeScale = gameSpeed;
    }

    // This function resets all level elements within a game session
    // This is being used when a player looses a ball, but has not lost all of them yet
    public void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Trigger this when the player enters a trigger area in the middle of the disco sphere
    public void lostBall()
    {
        lives--;
        if (lives < 0)
        {
            gameIsLost();
        }
    }

    // This is being triggered if a player has lost his final life
    // This reloads the whole scene and the game begins again
    public void gameIsLost()
    {
        Debug.Log("You have lost");
        reloadLevel();
    }

    public void toggleMusic()
    {
        
    }

    public void toggleBottomMenu() {
        
        bottomMenu.SetActive(!bottomMenu.activeSelf);

        // Hide startMenu if it was active
        if(startMenu.activeSelf == true) {
            startMenu.SetActive(false);
        }
        
        // Hide leaderboards if it was active
        if(leaderboards.activeSelf == true) {
            leaderboards.SetActive(false);
        }
    }

    public void toggleStartMenu() {
        startMenu.SetActive(!startMenu.activeSelf);
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

    public float getGameSpeed()
    {
        return gameSpeed;
    }

    public void setGameSpeed(float gameSpeed)
    {
        this.gameSpeed = gameSpeed;
    }

    // This method is connected to the OnDestroyed event of a BrickController
    // generated in the game. When called, it will schedule all of the provided
    // effects of that brick to be played
    public void OnBlockDestroyed(BrickEffect[] effects)
    {
        foreach (BrickEffect effect in effects)
        {
            StartCoroutine(ExecuteBlockEffect(effect));
        }
    }

    private IEnumerator ExecuteBlockEffect(BrickEffect effect)
    {
        // TODO Update UI (show icon for the effect)

        yield return effect.Apply();

        // TODO Update UI (remove icon for the effect)
    }
}
