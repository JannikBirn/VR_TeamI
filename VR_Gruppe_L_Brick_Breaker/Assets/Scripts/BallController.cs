using UnityEngine;

public class BallController : MonoBehaviour
{

    public Vector3 direction = new Vector3(0f, 0f, 2.5f);

    private GameObject player;

    void Awake()
    {
        // Get the player object (so that we know their position at all times)
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        // Move along the current direction
        transform.position += direction * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contactCount > 0)
        {
            ContactPoint contactPoint = collision.contacts[0];

            if (collision.collider.CompareTag("Platform"))
            {
                // TODO When colliding with the player, the normal vector might be different.
                // Depending on the angle, change the normal so that the ball is reflected differently at the edges of the platform.
                // Also, take the player's velocity into account (head movement forward -> higher impact)

                // When the ball collides with the platform,
                // change its direction using accurate physics
                // (https://math.stackexchange.com/a/13263)
                // Calculate reflection vector and ball's new direction
                Vector3 normal = contactPoint.normal;
                Vector3 reflection = direction - 2 * (Vector3.Dot(direction, normal)) * normal;
                this.direction = reflection;
            }
            else if (collision.collider.CompareTag("Brick"))
            {
                // When the ball collides with a brick, always reflect it back towards the player,
                // no matter what the previous direction was. Keep the previous speed however,
                // so normalize the reflected vector and scale it up again
                Vector3 reflection = player.transform.position - contactPoint.point;
                float speed = direction.magnitude;
                this.direction = reflection.normalized * speed;
            }
        }
    }
}
