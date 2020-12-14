using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{

    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleEndScreen()
    {
        endScreen.SetActive(!endScreen.activeSelf);
    }

    public void onLevelEvent(int levelEvent)
    {
        switch (levelEvent)
        {
            case LevelEvent.LEVEL_START:
                endScreen.SetActive(false);
                break;
            case LevelEvent.LEVEL_PLAY:
                break;
            case LevelEvent.LEVEL_PAUSE:
                break;
            case LevelEvent.LEVEL_STOP:
                toggleEndScreen();
                break;
        }
    }
}
