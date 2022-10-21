using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Applies an explosion force to all nearby rigidbodies
public class ExplosionMenu : MonoBehaviour
{
    [SerializeField] private ExplosionType explosionType;

    private void Start()
    {
        Explode();
    }

    private void Explode()
    {


        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionType.radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionType.power, explosionPos, explosionType.radius, explosionType.upwardsModifier);
        }
    }
}