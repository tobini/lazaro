using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float runningSpeedAddition;

    public delegate void OnWalkingAction();
    public static event OnWalkingAction OnWalking;

    public delegate void OnRunningAction();
    public static event OnRunningAction OnRunning;

    public delegate void OnStoppedAction();
    public static event OnStoppedAction OnStopped;

    private Rigidbody2D playerRB;
    private Vector2 lastPlayerPosition;

	void Start () {
        playerRB = GetComponent<Rigidbody2D>();
        lastPlayerPosition = playerRB.position;
    }

    void FixedUpdate()
    {
        MovePlayer();
        TriggerMovementEvents();
        SavePlayerPosition();
    }

    private void SavePlayerPosition()
    {
        lastPlayerPosition = playerRB.position;
    }

    private void TriggerMovementEvents()
    {
        if (playerHasStoppedMoving())
        {
            OnStopped();
        }
        else if (Input.GetButton("Run"))
        {
            OnRunning();
        } else
        {
            OnWalking();
        }
    }

    private bool playerHasStoppedMoving()
    {
        return lastPlayerPosition == playerRB.position;
    }

    private void MovePlayer()
    {
        float movementSpeed = speed + Input.GetAxis("Run") * runningSpeedAddition;
        playerRB.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * movementSpeed, 0.7f), Mathf.Lerp(0, Input.GetAxis("Vertical") * movementSpeed, 0.7f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        WallCollisionSound wallCollisionSound = collision.gameObject.GetComponent<WallCollisionSound>();
        if (wallCollisionSound != null)
        {
            wallCollisionSound.PlaySound();
        }
    }
}


