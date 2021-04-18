using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenery : MonoBehaviour
{
    public float speed;
    void Start()
    {

    }


    void FixedUpdate()
    {
        transform.position += Vector3.back * Time.deltaTime * speed;
    }
}
