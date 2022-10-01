using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirdMayhemScript : MonoBehaviour
{

    [SerializeField] Rigidbody rbCell;

    // Start is called before the first frame update
    void Awake()
    {
        rbCell.detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rbCell.detectCollisions = true;
        }
        
    }
}
