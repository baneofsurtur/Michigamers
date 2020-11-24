﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;
    public int inputQueueSize = 7;
    public float jumpHeight;
    public float beatsPerMinute;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    private float movement = 0f;
    private float secondsPerBeat;
    private float secondsPerHalfBeat;
    private bool isTouchingGround;
    private Rigidbody2D rigidBody;
    private float jumpSpeedPrivate;
    private float newAcceleration;
    private Queue<bool> inputQueue;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        secondsPerBeat = 60f / beatsPerMinute;
        secondsPerHalfBeat = secondsPerBeat / 2f;

        inputQueue = new Queue<bool>(inputQueueSize);

        // Given fixed jump height, calculate gravity scale needed to fall from height in half a beat
        newAcceleration = (2f * -jumpHeight) / Mathf.Pow(secondsPerHalfBeat, 2f);
        rigidBody.gravityScale = newAcceleration / Physics2D.gravity.y;

        // Using the new gravity scale, determine initial velocity needed to reach the fixed jump height in time
        jumpSpeedPrivate = (2 * jumpHeight) / secondsPerHalfBeat;
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumpPressed = Input.GetButtonDown("Jump");
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (inputQueue.Count == inputQueueSize)
        {
            inputQueue.Dequeue();
        }

        inputQueue.Enqueue(isJumpPressed);

        if (isTouchingGround)
        {
            if (inputQueue.Contains(true))
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeedPrivate);
                inputQueue.Clear();
            }
        }

        // TODO: remove Support for moving the player left and right when the code that moves the level itself is checked in
        movement = Input.GetAxis("Horizontal");
        if (movement == 0f)
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        }
    }
}
