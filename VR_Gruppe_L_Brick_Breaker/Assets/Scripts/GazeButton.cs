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
        myTextMesh = GetComponentInChildren<TextMesh>();
        myTextMesh.text = label;
        
        changeColor(offColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (gaze)
        {
            if (duration > timer)
            {
                timer += Time.unscaledDeltaTime;
                gazeImg.fillAmount = timer / duration;
            }
            else
            {
                On();
            }
        }
    }

    private void changeColor(Color color) {
        GetComponent<Renderer>().material.SetColor("_BaseColor", color);
    }

    public void On()
    {

        gaze = false;
        gazeImg.fillAmount = 0;

        changeColor(onColor);
        myEvent.Invoke();
    }
    public void Off()
    {
        changeColor(offColor);

        if (gaze)
        {
            gaze = false;
            gazeImg.fillAmount = 0;
        }
    }
    public void StartCount()
    {
        timer = 0;
        gaze = true;
        changeColor(waitColor);
    }
}



