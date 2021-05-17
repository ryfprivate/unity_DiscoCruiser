using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject[] wheels;
    public float speed = 0;
    private bool spawned;

    void Start()
    {
        spawned = false;
    }

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
            rotation = 180f;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation, transform.eulerAngles.z);
    }

    void FixedUpdate()
    {
        transform.position += Vector3.back * Time.deltaTime * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("collission" + other.gameObject.tag);
        if (other.gameObject.tag == "DeletePortal")
        {
            if (!spawned)
            {

                spawned = true;
                return;
            }

            Destroy(gameObject);
        }

        if (other.gameObject.tag == "SpawnPortal")
        {
            if (!spawned)
            {

                spawned = true;
                return;
            }

            Destroy(gameObject);
        }
    }
}
