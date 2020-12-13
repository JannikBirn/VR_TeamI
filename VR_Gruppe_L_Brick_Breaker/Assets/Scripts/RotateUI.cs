using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{

    private Transform cameraTransform;
    private GameObject cam;
    private Vector3 axis = new Vector3(0,1,0);
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        angle = cam.transform.rotation.y - transform.rotation.y;

        Debug.Log(angle);

        if(angle > 0.2 || angle < -0.2) {
            transform.RotateAround(cam.transform.position, axis, angle);
        }
        
    }
}
