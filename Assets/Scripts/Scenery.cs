using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenery : MonoBehaviour
{
    private float speed;
    void Start()
    {
        speed = 2f;
    }

    void FixedUpdate()
    {
        transform.position += Vector3.back * Time.deltaTime * speed;
    }
}
