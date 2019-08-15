using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMover : MonoBehaviour
{
    public Transform obj, anchor;
    public float speed = 0.2f, dir = 1;

    void Update()
    {
        obj.RotateAround(anchor.position, new Vector3(0, 0, speed * dir * Time.deltaTime), 1);
        if (obj.rotation.z > 0.50f || obj.rotation.z < -0.50f)
        {
            dir *= -1;
        }
    }
}
