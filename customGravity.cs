using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class customGravity : MonoBehaviour
{
    public float mass = 5.0f;
    public static float globalGravity = -9.81f;
    public bool hasGravity = true;

    Rigidbody target;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGravity)
        {
            Vector3 gravity = globalGravity * mass * Vector3.up;
            target.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
