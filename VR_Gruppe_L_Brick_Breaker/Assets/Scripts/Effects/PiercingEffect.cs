using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingEffect : BrickEffect
{

    public float duration = 10f;

    public override IEnumerator Apply(BallController ballController)
    {
        // Turn on the effect
        ballController.isPiercing = true;

        // Wait for the effect to stop
        yield return new WaitForSeconds(duration);

        // Turn off again
        ballController.isPiercing = false;
    }
}
