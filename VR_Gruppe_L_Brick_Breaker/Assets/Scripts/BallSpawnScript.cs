using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnScript : MonoBehaviour
{
    public GameObject ballPrefab;

    public void onLevelEvent(int levelEvent)
    {
        if (levelEvent == LevelEvent.LEVEL_START)
        {
            Instantiate(ballPrefab, transform.position, Quaternion.identity);
        }
    }
}
