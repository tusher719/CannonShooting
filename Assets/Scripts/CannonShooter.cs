using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float launchVelocity = 1500f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && LevelManager.instance != null && LevelManager.instance.CanShoot())
        {
            Shoot();

            LevelManager.instance.BulletUsed();
        }
    }

    void Shoot()
    {
        GameObject launchedObject = Instantiate(bulletPrefab, transform.position, transform.rotation);
        launchedObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity, 0));
    }
}
