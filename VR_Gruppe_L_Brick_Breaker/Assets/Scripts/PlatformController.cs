using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    // Scale platform in X direction (width)
    private float scaleX;

    public Camera cam;
    public Transform cameraTransform;

    private float distance = 15;

    // Start is called before the first frame update
    void Start()
    {   
        cameraTransform = cam.transform;
        //cam = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {  
        keyboardInput();
        followGaze();
    }

    // Change the platform's scale
    public void setScale(float scaleX,float scaleY) {
        Debug.Log("Changing Size!");
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(scaleX,scaleY,1));
    } 

    // Keyboard inputs for debugging use only
    private void keyboardInput() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Changing Size!");
            setScale(2, 1);
        }
    }


    void followGaze() {
        transform.position = cameraTransform.position + cameraTransform.forward * distance;
        transform.rotation = cameraTransform.rotation;
    }
}
