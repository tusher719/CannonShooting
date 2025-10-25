using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetObject : MonoBehaviour
{
    [Header("Object Properties")]
    [SerializeField] float objectMass = 1f;
    [SerializeField] int objectScore = 10;
    
    private Rigidbody rb;
    private bool hasFallen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.mass = objectMass;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallDetector") && !hasFallen)
        {
            hasFallen = true;
            
            if (LevelManager.instance != null)
            {
                LevelManager.instance.AddScore(objectScore, transform.position);
                LevelManager.instance.TargetDestroyed();
            }

            Destroy(gameObject);
        }
    }
}