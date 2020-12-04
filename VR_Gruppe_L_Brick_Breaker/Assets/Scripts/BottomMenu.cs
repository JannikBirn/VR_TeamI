using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BottomMenu : MonoBehaviour
{

    public Color onColor;
    public Color offColor;
    public Color waitColor;

    public Image gazeImg;
    private bool gaze = false;
    private float timer;
    public float duration;
    public GameObject menu;
    private bool menuIsShowing = false;
    public GameManager manager;
     
    

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gaze) {
            if(duration > timer) {
                timer += Time.deltaTime;
                gazeImg.fillAmount = timer/duration;
            } else {
                timer = 0;
                if(menuIsShowing) {
                    Off();
                } else {
                    On();
                }
                
                gaze = false;
                gazeImg.fillAmount = 0;
            }
        }

    }

    public void On() {
        GetComponent<Renderer>().material.color = onColor;

        menuIsShowing = !menuIsShowing;
        menu.SetActive(true);
        manager.setGameSpeed(0.5f);
        
    }
    public void Off() {
        GetComponent<Renderer>().material.color = offColor;

        menuIsShowing = !menuIsShowing;
        menu.SetActive(false);
        manager.setGameSpeed(1f);

        if(gaze) {
            gaze = false;
            gazeImg.fillAmount = 0;
        }
    }
    public void StartCount() {
        timer = 0;
        gaze = true;
        gameObject.GetComponent<Renderer>().material.color = waitColor;
    }

    public void hoverQuitButton() {
        Debug.Log("You have hovered the quit Button");
    }
}
