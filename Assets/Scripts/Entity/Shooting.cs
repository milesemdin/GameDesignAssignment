using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    
    protected void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        
    }
    

    private void Update()
    {
        Shoot();
    }
}
