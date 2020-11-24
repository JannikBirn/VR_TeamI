using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public float time = 5f; //30 seconds for you
    private bool wasPlayed = false;
 
    public void Update()
    {
        if (time > 0) {
            time -= Time.deltaTime;
        }
        else {
            Debug.Log("Play Audio Here -- Timer Over!!");
            if(GetComponent<AudioSource>().isPlaying == false && !wasPlayed)
            {
                GetComponent<AudioSource>().Play();
                wasPlayed = true;
            }  
        }
        
    }
}
