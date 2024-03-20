using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
   public bool facingRight;
   public float bulletSpeed;

   private void Update()
   {
      int direction = 1;
      if (!facingRight)
      {
         direction = -1;
      }
      transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime * direction);
   }
}
