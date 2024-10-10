using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    private float maxInitialAngle = 0.67f;
    private float moveSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        InitialPush();
    }

    private void InitialPush()
    {
        Vector3 dir = Vector3.down;
        dir.y=Random.Range(-maxInitialAngle, maxInitialAngle);
        rb.velocity= dir*moveSpeed;
    }
}
