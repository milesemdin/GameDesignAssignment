using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float shootCooldown;
    private bool shootReady;

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
            bullet.GetComponent<BulletMovement>().facingRight = facingRight;

            Instantiate(bullet, transform.position, Quaternion.identity);

            shootReady = false;
            StartCoroutine(ShotCooldown(shootCooldown));
        }
        
    }

    private IEnumerator ShotCooldown(float duration)
    {
        yield return new WaitForSeconds(duration); // Delay here

        // Do stuff after delay
        shootReady = true;
    }
}
