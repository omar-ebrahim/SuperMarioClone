using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields & Constants

    private const string AXIS_HORIZONTAL = "Horizontal";
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private float inputAxis;
    private new Camera camera;

    #endregion

    #region Properties

    [SerializeField]
    private float moveSpeed = 8;

    #endregion

    private void Awake()
    {
        // Get this component's Rigidbody2D instance. It only has one.
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    // Runs every frame
    private void Update()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis(AXIS_HORIZONTAL);
        // Take current vel, move towards the new input value, over time - independent of frame rate
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
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
}
