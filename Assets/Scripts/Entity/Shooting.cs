using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float shootCooldown;
    private bool shootReady;
    public bool towardsPlayer;

    private void Start()
    {
        shootReady = true;
    }


    private void Update()
    {
        Shoot();
    }

    protected void Shoot()
    {
        if (shootReady && Input.GetKey(KeyCode.O))
        {
            bool facingRight = GetComponent<Entity>().facingRight;
            float xRotation = 0;

            if (!facingRight)
            {
                xRotation = 180;
            }

            if (towardsPlayer)
            {

            }

            Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, xRotation));

            shootReady = false;
            StartCoroutine(ShotCooldown(shootCooldown));
        }
        
    }

    private Quaternion RotationTowardsPlayer()
    {
        return Quaternion.identity;
    }

    private IEnumerator ShotCooldown(float duration)
    {
        yield return new WaitForSeconds(duration); // Delay here

        // Do stuff after delay
        shootReady = true;
    }
}
