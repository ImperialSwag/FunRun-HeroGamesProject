using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyMover : MonoBehaviour
{
    public Transform obj, anchor;
    public float speed = 0.2f, dir = 1;

    void FixedUpdate()
    {
        obj.RotateAround(anchor.position, new Vector3(0, speed * dir * Time.deltaTime, 0), 1);
    }
}
    