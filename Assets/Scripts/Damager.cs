using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    #region Variables

    [SerializeField] private int damage;
    [SerializeField] private TeamID.ID teamID;
    [SerializeField] private bool isProjectile;

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
        if (collisionTeamID != null)
        {
            if (teamID != collisionTeamID.teamID)
            {
                collider.GetComponent<Entity>().TakeDamage(5);
                if (isProjectile && !collider.GetComponent<Entity>().isInvulnerable)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    #endregion
}
