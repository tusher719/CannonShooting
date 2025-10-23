using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] float bulletLifetime = 5f;
    [SerializeField] float shootForce = 25f;
    [SerializeField] float bounceDamping = 0.5f;
    [SerializeField] int maxBounces = 5;
    [SerializeField] float minBounceVelocity = 0.2f;

    private Rigidbody rb;
    private int bounceCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(transform.forward * shootForce, ForceMode.Impulse);

        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounceCount++;

        if (bounceCount >= maxBounces)
        {
            Destroy(gameObject, 0.5f);
            return;
        }

        Vector3 velocity = rb.velocity;
        velocity.y = Mathf.Abs(velocity.y) * bounceDamping;
        rb.velocity = velocity;

        if (rb.velocity.magnitude < minBounceVelocity)
        {
            Destroy(gameObject, 1f);
        }
    }
}