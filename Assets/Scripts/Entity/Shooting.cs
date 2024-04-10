using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float shootCooldown;
    protected bool shootReady;
    public bool towardsPlayer;

    private void Start()
    {
        shootReady = true;
    }


    protected virtual void Update()
    {
        TakeInput();
    }

    protected virtual void TakeInput()
    {
        if (shootReady && Input.GetKey(KeyCode.O))
        {
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        bool facingRight = GetComponent<Entity>().facingRight;
        float xRotation = 0;

        if (!facingRight)
        {
            xRotation = 180;
        }

        if (towardsPlayer)
        {
            xRotation = XRotationTowardsPlayer();
        }

        Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, xRotation));

        shootReady = false;
        StartCoroutine(ShotCooldown(shootCooldown));
    }

    private float XRotationTowardsPlayer()
    {
        float xRotation = 0;

        // Add rotation code here

        return xRotation;
    }

    private IEnumerator ShotCooldown(float duration)
    {
        yield return new WaitForSeconds(duration); // Delay here

        // Do stuff after delay
        shootReady = true;
    }
}
