using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileHeight = 1f;
    [SerializeField] float timeToDestroy = 3f;

    private bool hasHit;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private float impactRadius;

    void Start()
    {
        Destroy(projectile, timeToDestroy);
    }


    void Update()
    {
        ProjectileTrajectory();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                if (hasHit)
                {
                    break;
                }
                hasHit = true;

                boxCollider.size = new Vector3(impactRadius, impactRadius, impactRadius);
                Debug.Log("Enemy HIT!");
                other.rigidbody.detectCollisions = false;
                other.gameObject.GetComponentInParent<EnemyMovementPawn>().GetKilled();
                break;
            //
            // default:
            // Debug.Log("Not HIT!");
            // break;
        }
    }

    private void ProjectileTrajectory()
    {
        Vector3 projectilePath = new Vector3(-projectileSpeed, projectileHeight, 0);
        transform.localPosition += projectilePath * Time.deltaTime;
    }
}