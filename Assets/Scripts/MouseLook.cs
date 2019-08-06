using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotation.y = transform.eulerAngles.y / lookSpeed;
        rotation.x = transform.eulerAngles.x / lookSpeed;
    }

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Quaternion q = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);

        Camera.main.transform.localRotation = q;
    }
}
