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

    private Vector3 _startTransformForward;

    void Start()
    {
        float randomX = Random.Range(0, 90f);
        float randomY = Random.Range(0, 90f);
        float randomZ = Random.Range(0, 90f);
        Vector3 randomVector = new Vector3(randomX, randomY, randomZ);
        rb.AddTorque(randomVector);

        Destroy(gameObject, timeToDestroy);
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/Shoot", transform.position);

        _startTransformForward = transform.forward;
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
                FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyHit", other.transform.position);
                other.rigidbody.detectCollisions = false;
                other.gameObject.GetComponentInParent<Enemy>().GetKilled();
                
                UIManager.Instance.AddToScore(1);
                break;
        }
    }

    private void ProjectileTrajectory()
    {
        // Vector3 projectilePath = new Vector3(-projectileSpeed, projectileHeight, 0);
        // transform.localPosition += projectilePath * Time.deltaTime;
        Vector3 projectilePath = projectileSpeed * _startTransformForward;
        projectilePath = new Vector3(projectilePath.x, projectileHeight, projectilePath.z);
        transform.localPosition += projectilePath *  Time.deltaTime;
    }
}