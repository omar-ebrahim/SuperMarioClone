using Assets.Scripts.Helpers;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    #region Private Fields

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    #endregion

    #region Public serialised fields

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private Vector2 direction = Vector2.left;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        // We don't want it moving right away
        enabled = false;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        // Unity's gravity
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        // Gravity is m/s^2, hence why we multiply by gravity a second time
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        if (rigidbody.RayCast(direction))
        {
            direction = -direction;
        }

        if (rigidbody.RayCast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }

    #endregion
}
