using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamID))]
public class Damager : MonoBehaviour
{
    #region Variables

    [SerializeField] private int damage;
    [SerializeField] private bool isProjectile;
    [SerializeField] private bool destroyProjectileOffScreen;

    #endregion

    #region Update

    private void Update()
    {
        if (isProjectile && destroyProjectileOffScreen)
        {
            DestroyOffScreen();
        }
    }

    #endregion

    #region DestroyOffScreen

    private void DestroyOffScreen()
    {
        Vector3 cameraPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        if (cameraPosition.x < 0 || cameraPosition.x > Screen.width || cameraPosition.y < 0 || cameraPosition.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Collisions

    // Doing collisions like this makes sure collision works with obstacles with or without rigidbodies

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollision(GameObject collider)
    {
        TeamID collisionTeamID = collider.GetComponent<TeamID>();
        TeamID teamID = GetComponent<TeamID>();
        if (collisionTeamID != null)
        {
            if (teamID.teamID != collisionTeamID.teamID)
            {
                if (isProjectile && !collider.GetComponent<Entity>().isInvulnerable)
                {
                    collider.GetComponent<Entity>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                else if (!collider.GetComponent<Entity>().isInvulnerable)
                {
                    collider.GetComponent<Entity>().TakeDamage(damage);
                }
            }
        }
    }

    #endregion
}
