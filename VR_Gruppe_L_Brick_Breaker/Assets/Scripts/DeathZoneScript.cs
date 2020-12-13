using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    private static readonly Color SPHERE_COLOR = new Color(1f, 0f, 0f, 0.3f);
    public GameManager gameManager;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            //Ball entered the death zone -> delete ball
            GameObject.Destroy(other.gameObject);

            //TODO fancier way of cheking if there is a ball left
            if (BallsHolderSingleton.Instance.balls.Count < 1)
            {
                //All balls are dead, call dead event in game manager
                gameManager.gameStop();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = SPHERE_COLOR;
        Gizmos.DrawSphere(this.transform.position, GetComponent<SphereCollider>().radius);
    }
}
