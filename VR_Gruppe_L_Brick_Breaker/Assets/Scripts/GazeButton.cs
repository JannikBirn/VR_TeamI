using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GazeButton : MonoBehaviour
{


    public Color onColor;
    public Color offColor;
    public Color waitColor;

    public Image gazeImg;
    private bool gaze = false;
    private float timer;
    public float duration;
    public GameManager manager;

    public UnityEvent myEvent;
    public string label;

    private TextMesh myTextMesh;


    // Start is called before the first frame update
    void Start()
    {   
        //TextMesh textObject = GameObject.Find("object2").GetComponent<TextMesh>();
        myTextMesh = GetComponentInChildren<TextMesh>();
        myTextMesh.text = label;
        GetComponent<Renderer>().material.color = offColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gaze) {
            if(duration > timer) {
                timer += Time.deltaTime;
                gazeImg.fillAmount = timer/duration;
            } else {
                On();
                gaze = false;
                gazeImg.fillAmount = 0;
            }
        }
    }

    public void On() {
        GetComponent<Renderer>().material.color = onColor;
        myEvent.Invoke();
    }
    public void Off() {
        GetComponent<Renderer>().material.color = offColor;

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
}



