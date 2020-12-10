using UnityEngine;

public class BallController : MonoBehaviour
{

    public Vector3 direction = new Vector3(0f, 0f, 2.5f);
    public bool isAutoPilot = false;

    public bool isPiercing = false;

    public Color colorAuto;
    public Color colorPiercing;

    private GameObject player;

    private Material material;
    private Color original;

    void Awake()
    {
        // Get the player object (so that we know their position at all times)
        player = GameObject.FindGameObjectWithTag("Player");

        material = GetComponent<Renderer>().material;
        original = material.color;
    }

    void FixedUpdate()
    {
        // Move along the current direction
        transform.position += direction * Time.deltaTime;

        SetColor();
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
                // When colliding with a brick, check if there is an auto-pilot effect
                // attached to that brick. If this is the case, immediately switch
                // on the auto pilot mode
                if (collision.collider.CompareTag("Brick") && collision.collider.GetComponent<AutoPilotEffect>() != null)
                {
                    this.isAutoPilot = true;
                }

                // During auto pilot, reflect the ball back along the normal vector
                // and don't send it back to the player
                if (this.isAutoPilot)
                {
                    // Change its direction using accurate physics
                    // (https://math.stackexchange.com/a/13263)
                    // Calculate reflection vector and ball's new direction
                    Vector3 normal = contactPoint.normal;
                    this.direction = direction - 2 * (Vector3.Dot(direction, normal)) * normal;
                }
                else
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

    private void SetColor(){
        if (isAutoPilot)
        {
            material.color = colorAuto;
        } else if (isPiercing)
        {
            material.color = colorPiercing;
        }
        else
        {
            material.color = original;
        }
    }
}
