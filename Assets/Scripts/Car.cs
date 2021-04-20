using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject[] wheels;
    private float speed = 0;

    void Update()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].transform.Rotate(2f, 0, 0);
        }
    }

    public void StartUp(float newSpeed, bool isForward)
    {
        float rotation;

        speed = newSpeed;

        if (isForward)
        {
            rotation = 0f;
        }
        else
        {
            Debug.Log("is not forward");
            rotation = 180f;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
    }

    void FixedUpdate()
    {
        transform.position += Vector3.back * Time.deltaTime * speed;
    }
}
