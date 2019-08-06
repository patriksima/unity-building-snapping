using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float speed = 10f;

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        move = transform.TransformDirection(move);
        move *= speed;
        move *= Time.deltaTime;

        transform.position += move;
    }
}
