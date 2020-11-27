using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    // Scale platform in X direction (width)
    private float scaleX;
    public Material regularMaterial;
    public Material hitMaterial;

    private Camera cam;
    private Transform cameraTransform;
    private float distance = 15;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {   
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cameraTransform = cam.transform;

        // Set material
        GetComponent<Renderer>().material = regularMaterial;
    }

    // Update is called once per frame
    void Update()
    {  
        keyboardInput();
        followGaze();
    }

    void followGaze() {
        transform.position = cameraTransform.position + cameraTransform.forward * distance;
        transform.rotation = cameraTransform.rotation;
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            switchMaterial();
        }

    }



    private void OnCollisionEnter(Collision other) {
        Debug.Log("Platform was hit");

        if (other.collider.gameObject.tag == "Player") {
            Debug.Log("Player hit the platform");
            switchMaterial();
        }
    }

    public void switchMaterial() {
        Debug.Log("Switching Material");
        if (hit) {
            GetComponent<Renderer>().material = hitMaterial;
            hit = false;
        } else {
            GetComponent<Renderer>().material = regularMaterial;
            hit = true;
        }
    }
}
