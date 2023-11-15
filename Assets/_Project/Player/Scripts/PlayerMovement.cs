using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float radius = 3;
    [SerializeField] private float rotation = 10;
    [SerializeField] private Rigidbody ship;
    [Space]
    [SerializeField, Range(0f, 360f)] float orbitAngle;

    private Vector2 moveInput;



    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 look = transform.position - ship.position;
        ship.transform.up = look;

        orbitAngle = (orbitAngle + moveInput.x * Time.deltaTime * speed) % 360;

        float x = Mathf.Cos(orbitAngle * Mathf.Deg2Rad) * radius;
        float z = Mathf.Sin(orbitAngle * Mathf.Deg2Rad) * radius;

        Vector3 newPosition = new Vector3(x, z, 0);
        ship.MovePosition(newPosition);

        //Quaternion lookRotation = Quaternion.LookRotation(Vector3.zero - ship.position);
        //ship.MoveRotation(Quaternion.Slerp(ship.rotation, lookRotation, rotation * Time.deltaTime));
    }
}
