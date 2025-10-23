using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootForce = 500f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && LevelManager.instance.CanShoot())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        
        bulletRb.AddForce(firePoint.forward * shootForce);
        
        LevelManager.instance.BulletUsed();
    }
}