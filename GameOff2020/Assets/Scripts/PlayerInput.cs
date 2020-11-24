using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;
    public float movement = 0f;
    public float jumpSpeed;
    public float jumpHeight;
    public float beatsPerMinute;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    private float secondsPerBeat;
    private float secondsPerHalfBeat;
    private bool isTouchingGround;
    private Rigidbody2D rigidBody;
    private float jumpSpeedPrivate;
    private float newAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        secondsPerBeat = 60f / beatsPerMinute;
        secondsPerHalfBeat = secondsPerBeat / 2f;

        // Given fixed jump height, calculate gravity scale needed to fall from height in half a beat
        newAcceleration = (2f * -jumpHeight) / Mathf.Pow(secondsPerHalfBeat, 2f);
        rigidBody.gravityScale = newAcceleration / Physics2D.gravity.y;

        // Using the new gravity scale, determine initial velocity needed to reach the fixed jump height in time
        jumpSpeedPrivate = (2 * jumpHeight) / secondsPerHalfBeat;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isTouchingGround) {
            if (Input.GetAxis("Vertical") > 0)
            {
                rigidBody.position = new Vector2(rigidBody.position.x, jumpHeight);
            }
            if (Input.GetButtonDown("Jump"))
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeedPrivate);
            }
        }

        // TODO: remove Support for moving the player left and right when the code that moves the level itself is checked in
        movement = Input.GetAxis("Horizontal");
        if (movement != 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }
}
