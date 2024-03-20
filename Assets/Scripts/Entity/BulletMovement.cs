using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
   public float bulletSpeed;

   private void Update()
   {
      transform.Translate(new Vector2(bulletSpeed * Time.deltaTime, 0));
   }
}
