using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private float range;
    [SerializeField] private float movementRange;
    private State state;

    private enum State
    {
        Idle,
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

        if (true) // Add code for range check
        {
            state = State.Attack;
        }

        return false;
    }
}
