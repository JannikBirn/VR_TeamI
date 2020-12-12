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
    private Material myMaterial;


    // Start is called before the first frame update
    void Start()
    {
        myTextMesh = GetComponentInChildren<TextMesh>();
        myTextMesh.text = label;
        
        myMaterial = GetComponent<Renderer>().material;

        Debug.Log(myTextMesh.text + " " + myMaterial.color);
        Debug.Log("offColor: " + offColor);

        myMaterial.color = offColor;

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

    public void On()
    {

        gaze = false;
        gazeImg.fillAmount = 0;

        myMaterial.color = onColor;
        myEvent.Invoke();
    }
    public void Off()
    {
        myMaterial.color = offColor;

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
        gameObject.GetComponent<Renderer>().material.color = waitColor;
    }
}



