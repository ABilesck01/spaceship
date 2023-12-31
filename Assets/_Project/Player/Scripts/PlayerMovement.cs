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

    private float orbitAngle = 270;
    private Vector2 moveInput;
    private bool canMove = false;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        if(!canMove) return; 

        Vector3 look = transform.position - ship.position;
        ship.transform.up = look;

        orbitAngle = (orbitAngle + moveInput.x * Time.deltaTime * speed) % 360;

        float x = Mathf.Cos(orbitAngle * Mathf.Deg2Rad) * radius;
        float z = Mathf.Sin(orbitAngle * Mathf.Deg2Rad) * radius;

        Vector3 newPosition = new Vector3(x, z, 0);
        ship.MovePosition(newPosition);
    }

    public void SwitchMove(bool value)
    {
        canMove = value;
    }

    public void OnSelectShip(Transform t)
    {
        ship = t.GetComponent<Rigidbody>();
        SwitchMove(true);
    }
}
