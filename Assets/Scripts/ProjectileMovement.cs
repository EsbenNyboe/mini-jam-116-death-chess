using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileHeight = 1f;
    [SerializeField] float timeToDestroy = 3f;
    [SerializeField] Rigidbody rb;

    private bool hasHit;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private float impactRadius;

    void Start()
    {
        Vector3 randomVector = new Vector3(50f, 50f, 50f);
        rb.AddTorque(randomVector);

        Destroy(gameObject, timeToDestroy);
        
        // PLAY SOUND: SHOOT
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
                // PLAY SOUND: KILL
                other.rigidbody.detectCollisions = false;
                other.gameObject.GetComponentInParent<EnemyMovementPawn>().GetKilled();
                
                UIManager.Instance.AddToScore(1);
                break;
        }
    }

    private void ProjectileTrajectory()
    {
        Vector3 projectilePath = new Vector3(-projectileSpeed, projectileHeight, 0);
        transform.localPosition += projectilePath * Time.deltaTime;
    }
}