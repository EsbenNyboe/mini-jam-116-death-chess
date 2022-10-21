using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Applies an explosion force to all nearby rigidbodies
public class ExplosionExample : MonoBehaviour
{
    [SerializeField] private ExplosionType explosionType;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Explode();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {           
            SceneManager.LoadScene("ExplosionTest");
        }
        
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