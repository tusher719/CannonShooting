using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    [SerializeField] float forwardBoost = 5f; 
    [SerializeField] float minYVelocity = 0.1f;
    [SerializeField] int maxBounces = 5;

    private int bounceCount = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        bounceCount++;

        if (bounceCount >= maxBounces)
        {
            Destroy(gameObject, 1f);
            return;
        }
        
        if (rb.velocity.y < minYVelocity)
        {
            Vector3 forwardDir = Vector3.ProjectOnPlane(rb.velocity, Vector3.up).normalized;
            rb.AddForce(forwardDir * forwardBoost, ForceMode.Impulse);
        }
    }
}
