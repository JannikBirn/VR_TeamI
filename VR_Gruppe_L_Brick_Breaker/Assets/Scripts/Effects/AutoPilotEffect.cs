using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPilotEffect : BrickEffect
{

    // How long is the effect active (in seconds)
    [Range(1f, 60f)]
    public float duration = 10;

    private BallController ballController;

    void Start()
    {
        // Locate the ball in the scene and store a reference to its BallController
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        ballController = ball.GetComponent<BallController>();
    }

    public override IEnumerator Apply()
    {
        // Turn on auto pilot
        ballController.isAutoPilot = true;

        // Wait for the effect to stop
        yield return new WaitForSeconds(duration);

        // Turn off auto pilot again
        ballController.isAutoPilot = false;
    }
}
