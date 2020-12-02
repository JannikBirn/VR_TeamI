using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    // Scale platform in X direction (width)
    private float scaleX;
    public Material regularMaterial;
    public Material hitMaterial;

    //public Camera cam;
    public Transform cameraTransform;
    private float distance = 15;
    private bool hit = false;

    private Transform startTransform;


    // Smooth follow gaze
    //public Transform target;
    public float smoothTime = 0.1F;
    private Vector3 velocity = Vector3.zero;


    void Start()
    {   
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //cameraTransform = cam.transform;

        // Set material
        GetComponent<Renderer>().material = regularMaterial;

        startTransform = transform;

        Vector3 target = cameraTransform.position + cameraTransform.forward * distance;
        transform.position = target;
        
    }

    private void OnBecameVisible() {

    }

    // Update is called once per frame
    void Update()
    {  
        keyboardInput();
        followGaze();
    }
    void followGaze() {
        Vector3 target = cameraTransform.position + cameraTransform.forward * distance;
        transform.rotation = cameraTransform.rotation;

        if(cameraTransform.rotation.x >= 0.25 || cameraTransform.rotation.x <= -0.25) {
            setScale(0,0);
        } else {
            setScale(1,1);
        }

        // apply movement
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity,  smoothTime);        
    }


    // Change the platform's scale
    public void setScale(float scaleX,float scaleY) {
        //Debug.Log("Changing Size!");

        //transform.localScale = Vector3.Scale(transform.localScale, new Vector3(scaleX,scaleY,1));
        //transform.localScale = Vector3.Scale(startTransform.localScale, new Vector3(scaleX,scaleY,1));
        transform.localScale = new Vector3(scaleX,scaleY,1);
    } 

    // Keyboard inputs for debugging use only
    private void keyboardInput() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Changing Size!");
            setScale(2, 1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            switchMaterial();
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Platform was hit");

        if (other.collider.gameObject.tag == "Ball")
        {
            Debug.Log("Player hit the platform");
            StartCoroutine(OnPlatformHit());
        }
    }

    private IEnumerator OnPlatformHit()
    {
        switchMaterial();
        yield return new WaitForSeconds(0.1f);
        switchMaterial();
    }

    public void switchMaterial()
    {
        if (hit)
        {
            GetComponent<Renderer>().material = regularMaterial;
            hit = false;
        }
        else
        {
            GetComponent<Renderer>().material = hitMaterial;
            hit = true;
        }
    }
}
