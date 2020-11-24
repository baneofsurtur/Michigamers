using System.Collections;
using System.Collections.Generic;
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
    private SpriteRenderer spriteRenderer;
    private float jumpSpeedPrivate;
    private float newAcceleration;
    private float newGravity;
    private StopWatch stopWatch;
    private bool isRising = false;
    private bool isFalling = false;
    private bool hasJustLanded = false;
    private bool hasJustPeaked = false;

    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new StopWatch();
        //stopWatch = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StopWatch>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        secondsPerBeat = 60f / beatsPerMinute;
        secondsPerHalfBeat = secondsPerBeat / 2f;

        Debug.Log("gravity:");
        Debug.Log(Physics2D.gravity.y);

        Debug.Log("rigidBody.position:");
        Debug.Log(rigidBody.position);


        Debug.Log("secondsPerHalfBeat:");
        Debug.Log(secondsPerHalfBeat);

        // Given fixed jump height, calculate gravity scale needed to fall from height in half a beat
        newAcceleration = (2f * -jumpHeight) / Mathf.Pow(secondsPerHalfBeat, 2f);
        rigidBody.gravityScale = newAcceleration / Physics2D.gravity.y;

        Debug.Log("newAcceleration:");
        Debug.Log(newAcceleration);

        Debug.Log("rigidBody.gravityScale:");
        Debug.Log(rigidBody.gravityScale);

        newGravity = rigidBody.gravityScale * Physics2D.gravity.y;

        Debug.Log("newGravity");
        Debug.Log(newGravity);

        // Using the new gravity scale, determine initial velocity needed to reach the fixed jump height in time
        jumpSpeedPrivate = (2 * jumpHeight) / secondsPerHalfBeat;

        Debug.Log("jumpSpeedPrivate:");
        Debug.Log(jumpSpeedPrivate);

        Debug.Log("secondsPerBeat:");
        Debug.Log(secondsPerBeat);
    }

    // Update is called once per frame
    void Update()
    {
        // if last frame you were rising and this frame you are stationary or falling then you just peaked
        hasJustPeaked = isRising && (rigidBody.velocity.y <= 0);

        // if last frame you were falling and this frame you are stationary then you just landed
        hasJustLanded = isFalling && (rigidBody.velocity.y == 0);

        isRising = rigidBody.velocity.y > 0;
        isFalling = rigidBody.velocity.y < 0;

        if (hasJustPeaked)
        {
            Debug.Log("Just peaked at (Y, time) : (" + rigidBody.position.y + ", " + stopWatch.getTime() + ")");
            Debug.Log("isRising:" + isRising + "\nisFalling:" + isFalling + "\nhasJustLanded:" + hasJustLanded + "\nhasJustPeaked:" + hasJustPeaked);
        }

        /*if (hasJustLanded)
        {
            Debug.Log("Just landed at (Y, time) : (" + rigidBody.position.y + ", " + stopWatch.getTime() + ")");
            Debug.Log("isRising:" + isRising + "\nisFalling:" + isFalling + "\nhasJustLanded:" + hasJustLanded + "\nhasJustPeaked:" + hasJustPeaked);

            stopWatch.resetStopWatch();
        }*/

        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isTouchingGround) {
            if (stopWatch.isRunning() & !isRising)
            {
                Debug.Log("Touched the ground at (Y, time) : (" + rigidBody.position.y + ", " + stopWatch.getTime() + ")");
                Debug.Log("isRising:" + isRising + "\nisFalling:" + isFalling + "\nhasJustLanded:" + hasJustLanded + "\nhasJustPeaked:" + hasJustPeaked);
                stopWatch.stop();
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                rigidBody.position = new Vector2(rigidBody.position.x, jumpHeight);
            }
            if (Input.GetButtonDown("Jump"))
            {
                stopWatch.reset();
                Debug.Log("Jump! at (Y, time) : (" + rigidBody.position.y + ", " + stopWatch.getTime() + ")");
                Debug.Log("isRising:" + isRising + "\nisFalling:" + isFalling + "\nhasJustLanded:" + hasJustLanded + "\nhasJustPeaked:" + hasJustPeaked);

                stopWatch.start();
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeedPrivate);
            }
        }

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
