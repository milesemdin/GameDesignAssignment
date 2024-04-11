using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private float attackRange;
    [SerializeField] private float movementRange;
    [SerializeField] private float attackAndMoveRange;
    public State state;

    

    public enum State
    {
        Idle,
        Move,
        AttackAndMove,
        Attack,
    }


    protected override void TakeInput()
    {
        Movement();
    }

    protected override void Start()
    {
        base.Start();
        state = State.Idle;
    }

    protected override void Update()
    {
        base.Update();
        bool playerInRange = MovingRange();
        // Resetting variables at the end to virtualize key release
        horizontalInput = 0;
        jumpInput = false;
        dashInput = false;
    }

    private void Movement()
    {
        jumpInput = false;
        dashInput = false;

        if (state == State.Move)
        {
            GameObject player = GameObject.Find("TestPlayer");

            float xPositionDifference = player.transform.position.x - transform.position.x;
            
            int movementDirection = 0;

            if (xPositionDifference > 0)
            {
                movementDirection = 1;
            }
            else if (xPositionDifference < 0)
            {
                movementDirection = -1;
            }

            horizontalInput = movementDirection;
        }
        else if (state == State.AttackAndMove)
        {
            GameObject player = GameObject.Find("TestPlayer");

            float xPositionDifference = player.transform.position.x - transform.position.x;

            float movementDirection = 0;

            if (xPositionDifference > 0)
            {
                movementDirection = 0.5f;
            }
            else if (xPositionDifference < 0)
            {
                movementDirection = -0.5f;
            }

            horizontalInput = movementDirection;
        }

        /*
         * Notes
         * Change the "horizontalInput" variable to move the enemy (1 for right and -1 for left
         * Change "jumpInput" to true to make the enemy jump
         * Change "dashInput to true to make the enemy dash
         */
    }
     private bool MovingRange()
    {
        GameObject player = GameObject.Find("TestPlayer");

        if (Vector2.Distance(transform.position, player.transform.position) < attackRange) // Add code for range check
        {
            state = State.Attack;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < attackAndMoveRange) // Add code for range check
        {
            state = State.AttackAndMove;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < movementRange) // Add code for movemet check
        {
            state = State.Move;
        }

        return false;
    }
}
