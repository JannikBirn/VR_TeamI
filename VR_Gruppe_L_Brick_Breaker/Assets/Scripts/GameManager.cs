using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


[System.Serializable]
public class LevelEvent : UnityEvent<int>
{
    public const int LEVEL_START = 0, //When level is starting, needs to be reset
    LEVEL_PLAY = 1, //When level is playing again, after a pause
    LEVEL_PAUSE = 2, //Pause when level is pausing but not stopping
    LEVEL_STOP = 3; //Level finished => player died

    //Level States
    public static int state = 1; //State in wish the player is starting in

    public const int STATE_PLAYING = 0,
    STATE_IN_MENU = 1;

    public new void Invoke(int value)
    {
        //Setting the state debending on the event
        switch (value)
        {
            case LEVEL_START:
                state = STATE_PLAYING;
                break;
            case LEVEL_PLAY:
                state = STATE_PLAYING;
                break;
            case LEVEL_PAUSE:
                state = STATE_IN_MENU;
                break;
            case LEVEL_STOP:
                state = STATE_IN_MENU;
                break;
        }

        base.Invoke(value);
    }
}


public class GameManager : MonoBehaviour
{
    public LevelEvent onLevelEvent;
    private int hits;
    private float normalGameSpeed;
    private float gameSpeed;

    public PlatformController plattform;
    public GameObject leaderboards;
    public GameObject menu;
    public GameObject startMenu;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        normalGameSpeed = Time.timeScale;
        gameSpeed = normalGameSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Plattform shrinks in size after some hits
        if (hits > 3)
        {
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




    public void toggleMusic()
    {

    }

    public void toggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void toggleStartMenu()
    {
        startMenu.SetActive(!startMenu.activeSelf);
    }

    public void quitGame()
    {
        // Quit Game and open main menu scene
    }

    public void toggleLeaderboards()
    {
        leaderboards.SetActive(!leaderboards.activeSelf);
    }


    // Getters & Setters
    public int getHits()
    {
        return hits;
    }

    // Call this when the player hit another brick
    public void hit()
    {
        hits++;
    }

    public float getGameSpeed()
    {
        return gameSpeed;
    }

    public void setGameSpeed(float gameSpeed)
    {
        this.gameSpeed = gameSpeed;
    }

    //LEVEL STATES AND STUFF

    [ContextMenu("gameStart()")]
    public void gameStart()
    {
        Debug.Log("GameManager : gameStart()");
        //resetting all the objects so the player can play again
        //BlockMeshGen is will reset in the BLockGeneratorScript
        onLevelEvent.Invoke(LevelEvent.LEVEL_START);
    }

    [ContextMenu("gamePlay()")]
    public void gamePlay()
    {
        Debug.Log("GameManager : gamePlay()");
        //unpausing the gampleay if it is paused
        onLevelEvent.Invoke(LevelEvent.LEVEL_PLAY);

        setGameSpeed(normalGameSpeed);
    }

    [ContextMenu("gamePause()")]
    public void gamePause()
    {
        Debug.Log("GameManager : gamePause()");
        //Pausing the current gameplay
        onLevelEvent.Invoke(LevelEvent.LEVEL_PAUSE);

        setGameSpeed(0f);
    }

    [ContextMenu("gameStop()")]
    public void gameStop()
    {
        Debug.Log("GameManager : gameStop()");
        //stopping the gamplay and opening the scoreboard/menu
        onLevelEvent.Invoke(LevelEvent.LEVEL_STOP);
    }


    // This method is connected to the OnDestroyed event of a BrickController
    // generated in the game. When called, it will schedule all of the provided
    // effects of that brick to be played
    public void OnBlockDestroyed(BrickEffect[] effects, BallController ballController)
    {
        foreach (BrickEffect effect in effects)
        {
            StartCoroutine(ExecuteBlockEffect(effect, ballController));
        }
    }

    private IEnumerator ExecuteBlockEffect(BrickEffect effect, BallController ballController)
    {
        // TODO Update UI (show icon for the effect)

        yield return effect.Apply(ballController);

        // TODO Update UI (remove icon for the effect)
    }



    private void OnDestroy()
    {
        onLevelEvent.RemoveAllListeners();
    }
}
