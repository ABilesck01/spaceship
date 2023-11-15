using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float smoothRotation = 0.125f;
    [SerializeField] private Vector3 offset;


    private void FixedUpdate()
    {
        Vector3 localOffset = offset;

        // Rotate the offset according to the target's rotation
        Vector3 rotatedOffset = target.rotation * localOffset;


        Vector3 desiredPos = target.localPosition + rotatedOffset;
        Vector3 smooth = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.localPosition = smooth;

        Quaternion desiredRot = target.rotation;
        Quaternion smoothRot = Quaternion.Lerp(transform.rotation, desiredRot, smoothRotation);
        transform.rotation = smoothRot;
    }
}
