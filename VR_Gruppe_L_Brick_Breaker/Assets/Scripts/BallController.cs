using UnityEngine;

public class BallController : MonoBehaviour
{

    public Vector3 direction = new Vector3(0f, 0f, 2.5f);
    public bool isAutoPilot = false;
    public bool isPiercing = false;

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
                Vector3 platformCenterPosition = collision.transform.position;
                HandlePlatformCollision(platformCenterPosition, contactPoint);
            }
            else
            {
                // When colliding with a brick, perform some additional calculations
                bool isBrick = collision.collider.CompareTag("Brick");
                if (isBrick)
                {
                    if (collision.collider.GetComponent<AutoPilotEffect>() != null)
                    {
                        // check if there is an auto-pilot effect attached to that brick.
                        // If this is the case, immediately switch on the auto pilot mode
                        this.isAutoPilot = true;
                    }
                    else if (collision.collider.GetComponent<PiercingEffect>() != null)
                    {
                        // check if there is an auto-pilot effect attached to that brick.
                        // If this is the case, immediately switch on the auto pilot mode
                        this.isPiercing = true;
                    }
                }

                // During auto pilot, reflect the ball back along the normal vector
                // and don't send it back to the player
                if (this.isPiercing)
                {
                    // While in piercing mode, bricks should not change the ball's direction at all.
                    // Only return it when it isn't a brick
                    if (!isBrick)
                    {
                        ReturnToPlayer(contactPoint);
                    }
                }
                else if (this.isAutoPilot)
                {
                    // While in auto-pilot, reflect normally and don't force the ball back to the player
                    ReflectNormally(contactPoint);
                }
                else
                {
                    ReturnToPlayer(contactPoint);
                }
            }
        }
    }

    private void HandlePlatformCollision(Vector3 platformCenterPosition, ContactPoint contactPoint)
    {
        // When colliding with the player, the reflection axis depends
        // on where the ball hits the platform. The further away from the center,
        // the more this axis is twisted
        Vector3 centerNormal = contactPoint.normal;
        Vector3 contactPosition = contactPoint.point;

        // From the contact position, move along the platform's normal vector
        // and use the end point of that vector to create the reflection axis from the center point
        Vector3 reflectionAxis = (contactPosition + centerNormal) - platformCenterPosition;
        Vector3 normal = reflectionAxis.normalized;

        // When the ball collides with the platform,
        // change its direction using accurate physics
        // (https://math.stackexchange.com/a/13263)
        // Calculate reflection vector and ball's new direction
        Vector3 reflection = direction - 2 * (Vector3.Dot(direction, normal)) * normal;
        this.direction = reflection;
    }

    private void ReflectNormally(ContactPoint contactPoint)
    {
        // Change direction using accurate physics
        // (https://math.stackexchange.com/a/13263)
        // Calculate reflection vector and ball's new direction
        Vector3 normal = contactPoint.normal;
        this.direction = direction - 2 * (Vector3.Dot(direction, normal)) * normal;
    }

    private void ReturnToPlayer(ContactPoint contactPoint)
    {
        // When the ball collides with a brick, always reflect it back towards the player,
        // no matter what the previous direction was. Keep the previous speed however,
        // so normalize the reflected vector and scale it up again
        Vector3 reflection = player.transform.position - contactPoint.point;
        float speed = direction.magnitude;
        this.direction = reflection.normalized * speed;
    }
}
