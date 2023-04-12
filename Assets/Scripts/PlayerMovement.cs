using Assets.Scripts.Helpers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields & Constants


    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private float inputAxis;
    private new Camera camera;

    #endregion

    #region Properties

    [SerializeField]
    private float moveSpeed = 8f;
    [SerializeField]
    private float maxJumpHeight = 4.5f; // Maximum 5 units
    [SerializeField]
    private float maxJumpTime = 1f; // How long it takes to get to the maximum jump height

    public float JumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f); // The parabola for the jump
    public float Gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    /// <summary>
    /// Whether Mario is grounded or not.
    /// </summary>
    public bool Grounded { get; private set; }

    /// <summary>
    /// Whether Mario is jumping or not.
    /// </summary>
    public bool Jumping { get; private set; }

    /// <summary>
    /// Whether Mario is sliding or not.
    /// </summary>
    public bool Sliding => (inputAxis > 0f && velocity.x < 0f) | (inputAxis < 0f && velocity.x > 0f);

    /// <summary>
    /// Whether Mario is running or not (uses 0.3 instead of 0 as the XVel)
    /// </summary>
    public bool Running => Mathf.Abs(velocity.x) > 0.3f || Mathf.Abs(inputAxis) > 0.3f;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        // Get this component's Rigidbody2D instance. It only has one.
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();

        Grounded = rigidbody.RayCast(Vector2.down);

        if (Grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();
    }

    #endregion

    private void ApplyGravity()
    {
        // We're falling if we have negative velocity or not pressing the jump button
        bool falling = velocity.y < 0f || !Input.GetButton(Constants.ACTION_JUMP);
        float multiplier = falling ? 2f : 1f;

        // apply gravity and terminal velocity
        velocity.y += Gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, Gravity / 2f);
    }

    private void GroundedMovement()
    {
        // There may be a frame or two between pressing spacebar and the character moving, so base it off its velocity.
        // The velocity will be between ~0.x and 0.0 when stood still due to gravity being applied
        velocity.y = Mathf.Max(velocity.y, 0f);
        Jumping = velocity.y > 0f;

        if (Input.GetButtonDown(Constants.ACTION_JUMP))
        {
            velocity.y = JumpForce;
            Jumping = true;
        }
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis(Constants.AXIS_HORIZONTAL);
        // Take current vel, move towards the new input value, over time - independent of frame rate
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rigidbody.RayCast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        CalculateDirection();
    }

    /// <summary>
    /// Changes the player to face left or right depending on velocity.
    /// </summary>
    private void CalculateDirection()
    {
        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero; // face right
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // rotate on the y-axis 180 degrees
        }
    }

    // Runs at a given interval
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 distanceMoved = velocity * Time.fixedDeltaTime; // d = vt
        position += distanceMoved;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_ENEMY))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                // play around with this value, but the player bounces off the top of an enemy
                velocity.y = JumpForce / 2;
                Jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer(Constants.LAYER_POWERUP))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f; // Stop moving upwards
            }
        }
    }
}
