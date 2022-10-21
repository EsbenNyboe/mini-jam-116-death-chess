using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEventScript : MonoBehaviour
{
    public Transform ragdollTransform;
    public GameObject particleToSpawn;
    public GameObject particleToKill;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     EnemySpawnParticle();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     EnemyKilledParticle();
        // }

    }

    public void EnemySpawnParticle()
    {
        GameObject newParticle = Instantiate(particleToSpawn, transform);
        Destroy(newParticle, 1f);
    }

    public void EnemyKilledParticle()
    {
        GameObject newParticle = Instantiate(particleToKill, ragdollTransform);
        Destroy(newParticle, 2f);
    }
}
