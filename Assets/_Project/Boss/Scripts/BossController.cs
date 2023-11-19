using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float radius = 3;
    [SerializeField] private float rotation = 5;
    [SerializeField] private Rigidbody ship;
    [Space]
    [SerializeField] private AudioClip explosionClip;

    private float currentAngle;
    private float targetAngle;
    private float moveAxis;
    private bool isOnPosition = false;

    private void Start()
    {
        GetTargetAngle();
    }

    private void OnEnable()
    {
        BossHealth.OnBossDeath += BossHealth_OnBossDeath;
    }

    private void OnDisable()
    {
        BossHealth.OnBossDeath -= BossHealth_OnBossDeath;
    }

    private void BossHealth_OnBossDeath(object sender, System.EventArgs e)
    {
        Invoke(nameof(DestroyObject), 1f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject); 
    }

    private void GetTargetAngle()
    {
        int rand = Random.Range(0, 8);
        targetAngle = rand * 45;
    }

    private void Move()
    {
        if (isOnPosition) return;

        if(Mathf.Abs(currentAngle - targetAngle) < 0.1f)
        {
            isOnPosition = true;
            moveAxis = 0;
            return;
        }

        if (currentAngle < targetAngle)
        {
            moveAxis = 1;
        }
        else if(currentAngle > targetAngle)
        {
            moveAxis = -1;
        }
    }

    private void Update()
    {
        try
        {
            if (isOnPosition)
            {
                Invoke(nameof(SwitchPosition), 2f);
            }

            Move();

            Vector3 look = transform.position - ship.position;
            //ship.transform.up = look;

            currentAngle = (currentAngle + moveAxis * Time.deltaTime * speed) % 360;

            transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        }
        catch (System.Exception)
        {
        }

        
    }

    private void SwitchPosition()
    {
        GetTargetAngle();
        isOnPosition = false;
    }

}
