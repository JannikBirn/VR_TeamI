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

    private bool isVisible = false;

    private Transform startTransform;


    // Smooth follow gaze
    //public Transform target;
    public float smoothTime = 0.1F;
    private Vector3 velocity = Vector3.zero;

    private float currentScaleX = 1f;
    private float currentScaleY = 1f;

    void Start()
    {

        // Set material
        GetComponent<Renderer>().material = regularMaterial;

        startTransform = transform;

        Vector3 target = cameraTransform.position + cameraTransform.forward * distance;
        transform.position = target;

    }

    private void OnBecameVisible()
    {

    }

    // Update is called once per frame
    void Update()
    {
        keyboardInput();
        followGaze();
    }
    void followGaze()
    {
        Vector3 target = cameraTransform.position + cameraTransform.forward * distance;
        transform.rotation = cameraTransform.rotation;

        Vector3 hideVector = new Vector3(0 ,30,-10);

        if (cameraTransform.rotation.x >= 0.30 || cameraTransform.rotation.x <= -0.30)
        {
            // hide plattform
            transform.position = Vector3.SmoothDamp(transform.position, hideVector, ref velocity, smoothTime);
        }
        else
        {
            // Follow players gaze
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        }        
    }

    // Change the platform's scale
    public void setScale(float scaleX, float scaleY)
    {
        this.currentScaleX = scaleX;
        this.currentScaleY = scaleY;

        //Debug.Log("Changing Size!");
        transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    // Keyboard inputs for debugging use only
    private void keyboardInput()
    {
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

    public void setIsVisible() {
        isVisible = !isVisible;
    }

    public bool getIsVisible() {
        return isVisible;
    }
}
