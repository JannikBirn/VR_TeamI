using UnityEngine;

public class BallController : MonoBehaviour
{

    public Vector3 direction = new Vector3(0f, 0f, 2.5f);

    void FixedUpdate()
    {
        // Move along the current direction
        transform.position += direction * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contactCount > 0)
        {
            if (collision.collider.CompareTag("Player"))
            {
                // TODO When colliding with the player, the normal vector might be different.
                // Depending on the angle, change the normal so that the ball is reflected differently at the edges of the platform.
                // Also, take the player's velocity into account (head movement forward -> higher impact)
            }

            // Calculate reflection vector and ball's new direction
            // (https://math.stackexchange.com/a/13263)
            ContactPoint contactPoint = collision.contacts[0];
            Vector3 normal = contactPoint.normal;
            Vector3 reflection = direction - 2 * (Vector3.Dot(direction, normal)) * normal;
            this.direction = reflection;
        }
    }
}
