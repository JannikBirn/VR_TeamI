using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BrickDestroyedEvent : UnityEvent<BrickEffect[]> { }

public class BrickController : MonoBehaviour
{

    // Whether or not the brick can be destroyed by the ball
    public bool invincible = false;

    // The effect to play when the brick is destroyed
    public GameObject destroyEffectPrefab;

    // List of effects to trigger when the brick is destroyed
    //public List<BrickEffect> effects;

    // This event is sent when the brick is destroyed by the ball
    public BrickDestroyedEvent OnDestroyed;

    private BrickEffect[] effects;

    void Start()
    {
        // Collect all scripts attached to this GameObject, which are a subclass of "BrickEffect"
        effects = GetComponents<BrickEffect>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ball"))
        {
            // Destroy the brick upon collision, unless it cannot be destroyed
            if (invincible)
            {
                return;
            }

            // Notify listeners about all the destruction
            OnDestroyed.Invoke(effects);

            // Destroy the brick
            DestroyBrick();
        }
    }

    private void DestroyBrick()
    {
        // Play interaction effect (if any), then destroy yourself
        if (destroyEffectPrefab != null)
        {
            Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
