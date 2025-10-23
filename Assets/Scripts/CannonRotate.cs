using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotate : MonoBehaviour
{
    [SerializeField] Transform cannonHead;
    [SerializeField] float rotationSpeed = 50f;

    [Header("Rotation Limits")]
    [SerializeField] float minXAngle = 20f;
    [SerializeField] float maxXAngle = 100f;
    [SerializeField] float minYAngle = -20f;
    [SerializeField] float maxYAngle = 20f;

    float xRotation = 45f;
    float yRotation = 0f;

    void Update()
    {
        RotateCannonHead();
    }

    void RotateCannonHead()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY * rotationSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, minXAngle, maxXAngle);

        yRotation += mouseX * rotationSpeed * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, minYAngle, maxYAngle);

        cannonHead.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
