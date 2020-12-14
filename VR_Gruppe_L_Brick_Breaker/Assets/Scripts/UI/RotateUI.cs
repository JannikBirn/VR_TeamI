using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{

    private Transform cameraTransform;
    private GameObject cam;
    private Vector3 axis = new Vector3(0,1,0);
    private float angle;
    // public float smoothTime = 0.1F;
    // private Vector3 velocity = Vector3.zero;

    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        

        // angle = cam.transform.rotation.y - transform.rotation.y; 

        // if(angle > 0.3 || angle < -0.3) {
        //     transform.RotateAround(cam.transform.position, axis, angle);
        // }
        
    }
}
